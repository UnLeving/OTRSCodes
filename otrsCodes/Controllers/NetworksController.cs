using otrsCodes.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class NetworksController : Controller
    {
        private Model1 db = new Model1();

        [HttpGet]
        public ActionResult Index(int? id)
        {
            return PartialView(db.Countries.Find(id).Networks.ToList());
        }

        // GET: Networks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Networks networks = db.Networks.Find(id);
            if (networks == null)
            {
                return HttpNotFound();
            }
            return View(networks);
        }

        // GET: Networks/Create
        public ActionResult Create()
        {
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Networks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,ColorId,Name")] Networks networks)
        {
            if (ModelState.IsValid)
            {
                db.Networks.Add(networks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", networks.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", networks.CountryId);
            return View(networks);
        }

        // GET: Networks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Networks networks = db.Networks.Find(id);
            if (networks == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", networks.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", networks.CountryId);
            return View(networks);
        }

        // POST: Networks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId,ColorId,Name")] Networks networks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(networks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", networks.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", networks.CountryId);
            return View(networks);
        }

        // GET: Networks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Networks networks = db.Networks.Find(id);
            if (networks == null)
            {
                return HttpNotFound();
            }
            return View(networks);
        }

        // POST: Networks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Networks networks = db.Networks.Find(id);
            db.Networks.Remove(networks);
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
