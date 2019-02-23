using OfficeOpenXml;
using otrsCodes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CountriesController : Controller
    {
        private Model db = new Model();
        readonly string greyColorHEX = "#808080";

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult CodesTable(int countryId, string R = "0")
        {
            List<BaseTable> UIcodesTable = new List<BaseTable>();
            BaseTable table = null;
            // init table with default values
            for (int i = 0; i < 100; ++i)
            {
                table = new BaseTable
                {
                    R = R,
                    AB = i < 10 ? $"0{i}" : $"{i}"
                };
                for (int j = 0; j < 10; ++j)
                {
                    table.codes[j] = new CodeDt() { code = $"{table.AB}{j}" };
                }
                UIcodesTable.Add(table);
            }

            Country country = db.Countries.Find(countryId);
            ICollection<Code> countryCodes = country.Codes;
            // paint cells with roots colors
            if (R.Length > 1)
            {
                foreach (var ABrow in UIcodesTable)
                {
                    IEnumerable<Code> rootCodes = null;
                    string RAB;
                    for (int i = 1; i < R.Length; i++)
                    {
                        RAB = R + ABrow.AB;
                        if (i > 1)
                        {
                            RAB = RAB.Remove(RAB.Length - i + 1);
                        }
                        RAB = RAB.Substring(RAB.Length - 3);
                        rootCodes = countryCodes.Where(code => code.R == R.Remove(R.Length - i) && code.Value.Equals(RAB));
                        if (rootCodes.Count() > 0)
                            break;
                    }

                    if (rootCodes.Count() > 0)
                        foreach (var rootCode in rootCodes)
                        {
                            char lastDigit = rootCode.Value[rootCode.Value.Length - 1] == ' ' ?
                                                    rootCode.Value[rootCode.Value.Length - 2] :
                                                     rootCode.Value[rootCode.Value.Length - 1];
                            for (int i = 0; i < 10; ++i)
                            {
                                if (lastDigit == i.ToString()[0])
                                {
                                    for (int k = 0; k < 10; k++)
                                    {
                                        ABrow.codes[k].colorHEX = rootCode.Network.Color.Hex;
                                        ABrow.codes[k].id = -rootCode.Id;
                                    }
                                }
                                continue;
                            }
                        }
                }
            }

            // paint cells with inherited codes colors
            IEnumerable<Code> inheritedCodes = null;
            string RCodeValue;
            foreach (var ABrow in UIcodesTable)
            {
                foreach (var cell in ABrow.codes)
                {
                    RCodeValue = R + cell.code;
                    inheritedCodes = countryCodes.Where(code => $"{code.R}{code.Value}".StartsWith(RCodeValue));
                    if (inheritedCodes.Count() == 0) continue;
                    string colorHEX = null;
                    foreach (var code in inheritedCodes)
                    {
                        if (colorHEX == null)
                            colorHEX = code.Network.Color.Hex;
                        else if (colorHEX != code.Network.Color.Hex)
                        {
                            colorHEX = greyColorHEX;
                            break;
                        }
                    }
                    cell.colorHEX = colorHEX;
                }
            }

            // fill table with codes
            IEnumerable<Code> codesOfR = countryCodes.Where(code => code.R == R);
            if (codesOfR.Count() > 0)
            {
                foreach (var ABrow in UIcodesTable)
                {
                    foreach (var cell in ABrow.codes)
                    {
                        Code code = codesOfR.FirstOrDefault(_code => _code.Value == cell.code);
                        if (code != null)
                        {
                            cell.colorHEX = code.Network.Color.Hex;
                            cell.id = code.Id;
                        }
                    }
                }
            }

            return PartialView(UIcodesTable);
        }

        public ActionResult CodesList(int countryId)
        {
            Country country = db.Countries.Find(countryId);
            if (country == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Country not found");
            List<Code> codes = new List<Code>();
            foreach (var code in country.Codes.ToList())
            {
                codes.Add(new Code() { Value = $"{code.Country.Code}{code.R}{code.Value}" });
            }
            return PartialView(codes);
        }

        [HttpGet]
        public ActionResult CountryDropDown()
        {
            List<Country> countries = new List<Country>();
            foreach (var country in db.Countries)
            {
                countries.Add(new Country() { Id = country.Id, Name = $"{country.Code} {country.Name}" });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return PartialView();
        }

        [HttpGet]
        public ActionResult RegExp()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult RegExp(int? id)
        {
            var codes = db.Networks.Find(id)?.Codes;
            if (codes == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            string codeRegExp = $"^{codes.First().Country.Code}(";
            foreach (var item in codes)
            {
                codeRegExp += item.Value + "|";
            }
            codeRegExp = codeRegExp.TrimEnd('|');
            codeRegExp += ").*";

            return File(Encoding.UTF8.GetBytes(codeRegExp), "text/plain", $"{codes.First().Country.Name.Trim()} {codes.First().Network.Name.Trim()}.txt");
        }

        [HttpGet]
        public ActionResult ExportCodes()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ExportCodes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string country = db.Countries.Find(id).Name.Trim();
            var codes = db.Countries.Find(id).Codes;

            if (codes == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            List<CodeDT> list = new List<CodeDT>();
            foreach (var item in codes)
            {
                list.Add(new CodeDT() { Value = $"{item.Country.Code}{item.R}{item.Value}", Country = item.Country.Name, Network = item.Network.Name });
            }
            return ExportToExcel(list, $"{country} {DateTime.Now}");
        }

        FileStreamResult ExportToExcel(IEnumerable<CodeDT> dataSet, string fileName)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(dataSet, true);

            return File(new MemoryStream(excel.GetAsByteArray()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.Where(c => c.Name == country.Name).FirstOrDefault() == null)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Country {country.Name} already exist");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong model");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}