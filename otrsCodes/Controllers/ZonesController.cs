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
    public class ZonesController : Controller
    {
        private Model db = new Model();

        // GET: Zones
        public ActionResult Index()
        {
            var zones = db.Zones.Include(z => z.Country);
            return View(zones.ToList());
        }

        // GET: Zones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // GET: Zones/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Zones.Add(zone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", zone.CountryId);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", zone.CountryId);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", zone.CountryId);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zone zone = db.Zones.Find(id);
            db.Zones.Remove(zone);
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
