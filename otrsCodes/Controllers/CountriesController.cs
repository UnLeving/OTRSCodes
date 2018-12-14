using OfficeOpenXml;
using otrsCodes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CountriesController : Controller
    {
        private Model db = new Model();

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult CodesTable(int id = 0, int zone = 0)
        {
            List<BaseTable> dt1 = new List<BaseTable>();
            Country country = db.Countries.Find(id);
            List<Code> codes = null;
            if (country != null)
            {
                codes = country.Codes.Where(z => z.Zone == zone).ToList();
            }

            for (int i = 0; i < 100; ++i)
            {
                BaseTable table = new BaseTable();
                table.R = zone;
                table.AB = i;
                int val = /*zone * 1000 +*/ i * 10;

                for (int j = 0; j < 10; ++j)
                {
                    table.codes[j] = new CodeDt() { code = val + j };
                    if (codes != null)
                        foreach (var code in codes)
                        {
                            if (code.Value == val + j)
                                table.codes[j] = new CodeDt() { id = code.Id ,code = val + j, color = code.Network.Color.Hex };
                        }
                }
                dt1.Add(table);
            }

            return PartialView(dt1);
        }

        public ActionResult CountryDropDown()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return PartialView();
        }
        [HttpGet]
        public ActionResult RegExp()
        {
            return PartialView();
        }

        public ActionResult GetRegExp(int? id)
        {
           var codes = db.Networks.Find(id)?.Codes;
            if(codes==null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            string codeRegExp = $"^{codes.First().Country.Code}(";
            foreach (var item in codes)
            {
                codeRegExp += item.Value + "|";
            }
            codeRegExp.TrimEnd('|');
            codeRegExp += ").*";

            return Content(codeRegExp);
        }

        [HttpGet]
        public ActionResult ExportCodes()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                list.Add(new CodeDT() { Network = item.Network.Name, Code = $"{item.Country.Code}{item.Value}" });
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

        public ActionResult Details(int? id)
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