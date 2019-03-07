using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using otrsCode_netCore.Models;
using System.Linq;

namespace otrsCode_netCore.Controllers
{
    public class ColorsController : Controller
    {
        private Model db = new Model();

        public ActionResult Index()
        {
            return View(db.Colors.ToList());
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Hex")] Color color)
        {
            if (ModelState.IsValid)
            {
                if (db.Colors.Where(c => c.Hex == color.Hex).FirstOrDefault() == null)
                {
                    db.Colors.Add(color);
                    db.SaveChanges();
                    return new StatusCodeResult(StatusCodes.Status200OK);
                }
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Hex")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Color color = db.Colors.Find(id);
            db.Colors.Remove(color);
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
