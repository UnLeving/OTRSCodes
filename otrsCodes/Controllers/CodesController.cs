using otrsCodes.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace otrsCodes.Controllers
{
    public class CodesController : Controller
    {
        private Model db = new Model();

        [HttpPost]
        public ActionResult CreateMulti([Bind(Include = "CountryId,NetworkId,Zone,Value")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                foreach (var code in codes.Value)
                {
                    if (db.Codes.Where(c => c.Value == code).FirstOrDefault() == null)
                    {
                        db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone, Value = code });
                    }
                    else
                    {
                        db.Entry(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone, Value = code }).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Network not selected");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            db.Codes.Remove(code);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
