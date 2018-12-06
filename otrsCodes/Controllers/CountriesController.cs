using otrsCodes.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CountriesController : Controller
    {
        private Model db = new Model();

        public ActionResult Index(int id = 0, int zoneId = 0)
        {
            List<BaseTable> dt1 = new List<BaseTable>();
            Country country = db.Countries.Find(id);
            ICollection<Code> codes = null;
            if (country != null)
            {
                if (country.Zones.Count != 0)
                {
                    codes = country.Zones
                      .Where(zone => zone.Id == zoneId).First().Codes;
                }
            }

            for (int i = 0; i < 100; ++i)
            {
                BaseTable table = new BaseTable();
                table.R = zoneId;
                table.AB = i;
                int val = zoneId * 1000 + i * 10;

                for (int j = 0; j < 10; ++j)
                {
                    table.codes[j] = new CodeDt() { code = val + j };
                    if (codes != null)
                        foreach (var code in codes)
                        {
                            if (code.Value.Trim() == (val + j).ToString())
                                table.codes[j] = new CodeDt() { code = val + j, color = code.Network.Color.Hex };
                        }
                }
                dt1.Add(table);
            }

            return PartialView(dt1);
        }

        public ActionResult CountryList()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return PartialView(new SelectList(db.Countries, "Id", "Name"));
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        [HttpPost]
        public ActionResult AddCode([Bind(Include = "CountryId,NetworkId,ZoneId,Value")] Code codes)
        {
            if (ModelState.IsValid)
            {
                if (db.Zones.Find(codes.Id) == null)
                {
                    db.Zones.Add(new Zone() { Value = codes.ZoneId, CountryId = codes.CountryId });
                    db.SaveChanges();
                }
                if (db.Codes.Where(c=> c.Value == codes.Value).FirstOrDefault() == null)
                {
                    db.Codes.Add(codes);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Code {codes.Value} already exist");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Network not selected");
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

//public ActionResult Index()
//{
//    return View(db.Countries.ToList());
//}