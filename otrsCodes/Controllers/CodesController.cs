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
                var counter = 0;
                foreach (var code in codes.Value)
                {
                    var cd = db.Codes.Where(c => c.Value == code).FirstOrDefault();
                    if (cd == null)
                    {
                        db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone, Value = code });
                        ++counter;
                    }
                    else
                    {
                        if (cd.NetworkId == codes.NetworkId) continue;
                        cd.NetworkId = codes.NetworkId;
                        db.Entry(cd).State = EntityState.Modified;
                        ++counter;
                    }
                }
                if (counter == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nothing to change");
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Network not selected");
        }

        [HttpPost]
        public ActionResult Delete(int?[] ids)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code;
            foreach (var id in ids)
            {
                code = db.Codes.Find(id);
                if (code == null)
                {
                    continue;
                }
                db.Codes.Remove(code);
            }
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
