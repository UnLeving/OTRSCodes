using otrsCodes.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CountriesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Countries
        public ActionResult Index()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            ViewBag.NetworkId = new SelectList(db.Networks, "Id", "Name");
            return PartialView();
        }

        [HttpGet]
        public ActionResult Net(int? id)
        {
            return PartialView(db.Countries.Find(id).Networks.ToList());
        }

        public ActionResult Net()
        {
            return PartialView(db.Countries.Find(1).Networks.ToList());
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        //// GET: Countries/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Countries/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Code")] Countries countries)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Countries.Add(countries);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(countries);
        //}

        //// GET: Countries/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Countries countries = db.Countries.Find(id);
        //    if (countries == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(countries);
        //}

        //// POST: Countries/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Code")] Countries countries)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(countries).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(countries);
        //}

        //// GET: Countries/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Countries countries = db.Countries.Find(id);
        //    if (countries == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(countries);
        //}

        //// POST: Countries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Countries countries = db.Countries.Find(id);
        //    db.Countries.Remove(countries);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
