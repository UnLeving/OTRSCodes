using otrsCodes.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class ColorController : Controller
    {
        private Model db = new Model();

        // GET: Colors
        public ActionResult Index()
        {
            return View(db.Colors.ToList());
        }

        // GET: Colors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color colors = db.Colors.Find(id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
        }

        // GET: Colors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Hex")] Color colors)
        {
            if (ModelState.IsValid)
            {
                db.Colors.Add(colors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colors);
        }

        // GET: Colors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color colors = db.Colors.Find(id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Hex")] Color colors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colors);
        }

        // GET: Colors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color colors = db.Colors.Find(id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Color colors = db.Colors.Find(id);
            db.Colors.Remove(colors);
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
