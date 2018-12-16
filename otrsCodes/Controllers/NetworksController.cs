using otrsCodes.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class NetworksController : Controller
    {
        private Model db = new Model();

        public ActionResult Index(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var networks = db.Countries.Find(id).Networks;
            return PartialView(networks.ToList());
        }

        public ActionResult NetworkDropDown(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var networks = db.Countries.Find(id).Networks;
            ViewBag.NetworkId = new SelectList(networks, "Id", "Name");
            return PartialView();
        }

        public ActionResult Create()
        {
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,ColorId,Name")] Network network)
        {
            if (ModelState.IsValid)
            {
                if (db.Networks.Where(c => c.Name == network.Name).FirstOrDefault() == null)
                {
                    db.Networks.Add(network);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Network {network.Name} already exist");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong model");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", network.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", network.CountryId);
            return View(network);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId,ColorId,Name")] Network network)
        {
            if (ModelState.IsValid)
            {
                db.Entry(network).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", network.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", network.CountryId);
            return View(network);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return HttpNotFound();
            }
            return View(network);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Network network = db.Networks.Find(id);
            db.Networks.Remove(network);
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
