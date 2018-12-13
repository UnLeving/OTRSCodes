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
    public class CodesController : Controller
    {
        private Model db = new Model();

        // GET: Codes
        public ActionResult Index()
        {
            var codes = db.Codes.Include(c => c.Country).Include(c => c.Network).Include(c => c.Zone);
            return View(codes.ToList());
        }

        // GET: Codes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // GET: Codes/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            ViewBag.NetworkId = new SelectList(db.Networks, "Id", "Name");
            return View();
        }

        // POST: Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,CountryId,NetworkId,ZoneId")] Code code)
        {
            if (ModelState.IsValid)
            {
                db.Codes.Add(code);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", code.CountryId);
            ViewBag.NetworkId = new SelectList(db.Networks, "Id", "Name", code.NetworkId);
            return View(code);
        }

        // GET: Codes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", code.CountryId);
            ViewBag.NetworkId = new SelectList(db.Networks, "Id", "Name", code.NetworkId);
            return View(code);
        }

        // POST: Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,CountryId,NetworkId,ZoneId")] Code code)
        {
            if (ModelState.IsValid)
            {
                db.Entry(code).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", code.CountryId);
            ViewBag.NetworkId = new SelectList(db.Networks, "Id", "Name", code.NetworkId);
            return View(code);
        }

        // POST: Codes/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            db.Codes.Remove(code);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
