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
        private Model _db = new Model();

        [HttpPost]
        public ActionResult CreateMulti([Bind(Include = "CountryId,NetworkId,Zone,Value")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                ushort _addedCodes = 0;

                // TODO: преобразовать выражение для сравнения коллекций ??
                foreach (var code in codes.Value)
                {
                    var codeInDb = _db.Codes.Where(c => c.Zone == codes.Zone && c.Value == code).FirstOrDefault();
                    if (codeInDb == null)
                    {
                        #region Reduce codes
                        if (codes.Zone > 10)
                        {
                            if (codes.Value.Count() == 1)
                            {
                                var _inLineDBCodes = _db.Codes.Where(c => c.Zone == codes.Zone && c.Value.StartsWith(code.Remove(code.Length - 1)));
                                if(_inLineDBCodes.Count() == 9)
                                {
                                    string _parentCode = codes.Zone % 10 + code.Remove(code.Length - 1);
                                    _db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone / 10, Value = _parentCode });
                                    ++_addedCodes;
                                    _db.Codes.RemoveRange(_inLineDBCodes);
                                    break;

                                    // TODO: triger table update
                                }
                            }
                            else if (codes.Value.Count() == 10)
                            {
                                string _parentCode = codes.Zone % 10 + code.Remove(code.Length - 1);
                                _db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone / 10, Value = _parentCode });
                                ++_addedCodes;
                                break;
                            }
                        }
                        #endregion
                        _db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, Zone = codes.Zone, Value = code });
                        ++_addedCodes;
                    }
                    else
                    {
                        if (codeInDb.NetworkId == codes.NetworkId) continue;
                        codeInDb.NetworkId = codes.NetworkId;
                        _db.Entry(codeInDb).State = EntityState.Modified;
                        ++_addedCodes;
                    }
                }
                if (_addedCodes == 0)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nothing to change");

                _db.SaveChanges();
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
                code = _db.Codes.Find(id);
                if (code == null)
                {
                    continue;
                }
                _db.Codes.Remove(code);
            }
            _db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
