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
        private Model1 db = new Model1();

        // GET: Codes
        public ActionResult Index()
        {
            return View(db.Codes.ToList());
        }

        // GET: Codes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codes codes = db.Codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            return View(codes);
        }

        // GET: Codes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,NetworkId,Code")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                db.Codes.Add(codes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codes);
        }

        // GET: Codes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codes codes = db.Codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            return View(codes);
        }

        // POST: Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId,NetworkId,Code")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codes);
        }

        // GET: Codes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codes codes = db.Codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            return View(codes);
        }

        // POST: Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Codes codes = db.Codes.Find(id);
            db.Codes.Remove(codes);
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
