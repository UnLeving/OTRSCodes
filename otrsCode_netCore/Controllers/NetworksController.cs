using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using otrsCode_netCore.Models;
using System.Linq;

namespace otrsCode_netCore.Controllers
{
    public class NetworksController : Controller
    {
        private ModelContext db;

        public NetworksController(ModelContext context)
        {
            db = context;
        }

        public ActionResult Index(int? id)
        {
            if (id == null) return new StatusCodeResult(StatusCodes.Status400BadRequest);
            var networks = db.Countries.Find(id).Networks;
            return PartialView(networks.ToList());
        }

        public ActionResult NetworkDropDown(int? countryId)
        {
            if (countryId == null) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var networks = db.Countries.Find(countryId).Networks;
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
        public ActionResult Create([Bind("Id,CountryId,ColorId,Name")] Network network)
        {
            if (ModelState.IsValid)
            {
                if (db.Networks.Where(c => c.Name == network.Name).FirstOrDefault() == null)
                {
                    db.Networks.Add(network);
                    db.SaveChanges();
                    return new StatusCodeResult(StatusCodes.Status200OK);
                }
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return NotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex", network.ColorId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", network.CountryId);
            return View(network);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,CountryId,ColorId,Name")] Network network)
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
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            Network network = db.Networks.Find(id);
            if (network == null)
            {
                return NotFound();
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
