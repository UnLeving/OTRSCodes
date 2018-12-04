using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using otrsCodes.Models;

namespace otrsCodes.Controllers
{
    public class CountriesController : Controller
    {
        private Model db = new Model();

        // GET: Countries
        //public ActionResult Index()
        //{
        //    return View(db.Countries.ToList());
        //}

        public ActionResult Index(int id = 0, int zoneId = 0)
        {
            List<BaseTable> dt1 = new List<BaseTable>();
            //Country country = db.Countries.Find(id);
            //if (country != null)
            //{
            //    var codes = country.Zones
            //        .Where(zone=>zone.Id==zoneId).First().Codes;

            //    foreach (var code in codes)
            //    {
            //       var colorHex = code.Networks.Colors.Hex;
            //    }
            //}
            int val;
            for (int i = 0; i < 100; ++i)
            {
                BaseTable table = new BaseTable();
                table.R = zoneId;
                table.AB = i;
                val = table.R * 1000 + i * 10;

                for (int j = 0; j < table.codes.Length; ++j)
                {
                    table.codes[j] = new CodeDt() { code = val + j };
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

        // GET: Countries/Details/5
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

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        public ActionResult AddCode([Bind(Include = "CountryId,NetworkId,Zone,Code")] Code codes)
        {
            if (ModelState.IsValid)
            {
                if (db.Zones.Find(codes.Id) == null)
                {
                    db.Zones.Add(new Zone() { Id = codes.ZoneId, CountryId = codes.CountryId });
                    db.SaveChanges();
                }
                db.Codes.Add(codes);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Countries/Edit/5
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

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Countries/Delete/5
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

        // POST: Countries/Delete/5
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
