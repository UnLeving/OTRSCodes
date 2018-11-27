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

        public ActionResult CountryList()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View(new SelectList(db.Countries, "Id", "Name"));
        }

        public ActionResult Index(int num = 0)
        {
            //TODO:complete this
            db.Countries.Find(num).Codes.ToList();
            List<BaseTable> dt1 = new List<BaseTable>();
            for (int i = 0; i < 100; i++)
            {
                BaseTable table = new BaseTable();
                table.R = num;
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

        [HttpPost]
        public ActionResult AddCode([Bind(Include = "CountryId,NetworkId,Code")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                db.Codes.Add(codes);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CountryId,NetworkId,Code")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codes).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
