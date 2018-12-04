using otrsCodes.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CountryController : Controller
    {
        private Model db = new Model();

        public ActionResult CountryList()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return PartialView(new SelectList(db.Countries, "Id", "Name"));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return View(country);
            }

            return null;
        }

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

                for(int j = 0; j < table.codes.Length; ++j)
                {
                    table.codes[j] = new CodeDt() { code = val + j };
                }
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
            Country countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
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

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CountryId,NetworkId,Code")] Code codes)
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
