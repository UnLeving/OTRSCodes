﻿using otrsCodes.Models;
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

            List<BaseTable> dt1 = new List<BaseTable>();
            for (int i = 0; i < 100; i++)
            {
                BaseTable table = new BaseTable();
                table.R = 6;
                table.AB = i;
                int val = table.R * 1000 + i * 10;
                table.a = val + 0;
                table.b = val + 1;
                table.c = val + 2;
                table.d = val + 3;
                table.e = val + 4;
                table.f = val + 5;
                table.g = val + 6;
                table.h = val + 7;
                table.k = val + 8;
                table.l = val + 9;
                dt1.Add(table);
            }
            return PartialView(dt1);
        }

        [HttpGet]
        public ActionResult Net(int? id)
        {
            return PartialView(db.Countries.Find(id).Networks.ToList());
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

        // POST: Countries/Edit/5
        [HttpPost]
        public ActionResult Edit(Countries countries, int countryid, int networkid, int code, string hex)
        {
            if (ModelState.IsValid)
            {
                var t = db.Countries.Find(countryid).Networks.Where(i=>i.Id== networkid).First();

                db.Entry(countries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countries);
        }

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
