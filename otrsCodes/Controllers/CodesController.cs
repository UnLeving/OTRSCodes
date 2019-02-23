using otrsCodes.Models;
using System.Collections.Generic;
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
        public ActionResult CreateMulti([Bind(Include = "CountryId,NetworkId,R,Values")] Codes codes)
        {
            if (ModelState.IsValid)
            {
                ushort _addedCodes = 0;

                // TODO: преобразовать выражение для сравнения коллекций ??
                foreach (var code in codes.Values)
                {
                    var codeInDb = _db.Codes.Where(c => c.R == codes.R && c.Value == code).FirstOrDefault();
                    if (codeInDb == null)
                    {
                        #region Reduce codes
                        if (codes.R.Length >= 2)
                        {
                            if (codes.Values.Count() == 1)
                            {
                                if (codes.R.Remove(codes.R.Length - 1).Length > 0)
                                {
                                    string _zone = codes.R;
                                    string _code = code;
                                    Dictionary<string, string> _keyValuePairs = new Dictionary<string, string>();
                                    while (_zone.Remove(_zone.Length - 1).Length > 0)
                                    {
                                        _code = $"{_zone[_zone.Length - 1]}{_code.Remove(_code.Length - 1)}";
                                        _keyValuePairs.Add(_zone.Remove(_zone.Length - 1), _code);
                                        _zone = _zone.Remove(_zone.Length - 1);
                                    }
                                    // retreive root code
                                    Code _rootCode = null;
                                    foreach (var item in _keyValuePairs)
                                    {
                                        _rootCode = _db.Codes.Where(c => c.R == item.Key && c.Value == item.Value).FirstOrDefault();
                                    }

                                    if (_rootCode != null)
                                    {
                                        int _networkId = 0;
                                        // expand _rootCode
                                        string t = $"{codes.R}{code}".Remove(0, $"{_rootCode.R}{_rootCode.Value}".Length);
                                        for (int i = 0; i <= 9; i++)
                                        {
                                            // codes painting
                                            if (i.ToString()[0] != t[0])
                                                _networkId = _rootCode.NetworkId;
                                            else
                                                _networkId = codes.NetworkId;

                                            _db.Codes.Add(new Code()
                                            {
                                                CountryId = codes.CountryId,
                                                NetworkId = _networkId,
                                                R = $"{_rootCode.R}{_rootCode.Value[0]}",
                                                Value = $"{_rootCode.Value[1]}{_rootCode.Value[2]}{i}"
                                            });
                                        }
                                        // add new code
                                        _db.Codes.Add(new Code()
                                        {
                                            CountryId = codes.CountryId,
                                            NetworkId = codes.NetworkId,
                                            R = codes.R,
                                            Value = code
                                        });

                                        // delete _rootCode
                                        if (_db.Codes.Remove(_rootCode) != null)
                                        {
                                            ++_addedCodes;
                                            break;
                                        }
                                    }
                                }

                                var _inLineDBCodes = _db.Codes.Where(c =>
                                c.NetworkId == codes.NetworkId &&
                                c.R == codes.R &&
                                c.Value.StartsWith(code.Remove(code.Length - 1)));
                                if (_inLineDBCodes.Count() == 9)
                                {
                                    string _parentCode = codes.R[codes.R.Length - 1] + code.Remove(code.Length - 1);
                                    _db.Codes.Add(new Code()
                                    {
                                        CountryId = codes.CountryId,
                                        NetworkId = codes.NetworkId,
                                        R = codes.R.Remove(codes.R.Length - 1),
                                        Value = _parentCode
                                    });
                                    ++_addedCodes;
                                    _db.Codes.RemoveRange(_inLineDBCodes);
                                    break;

                                    // TODO: triger table update
                                }
                            }
                            else if (codes.Values.Count() == 10)
                            {
                                string _parentCode = codes.R[codes.R.Length - 1] + code.Remove(code.Length - 1);
                                _db.Codes.Add(new Code()
                                {
                                    CountryId = codes.CountryId,
                                    NetworkId = codes.NetworkId,
                                    R = codes.R.Remove(codes.R.Length - 1),
                                    Value = _parentCode
                                });
                                ++_addedCodes;
                                break;
                            }
                        }
                        #endregion
                        var ccc = _db.Codes.Add(new Code() { CountryId = codes.CountryId, NetworkId = codes.NetworkId, R = codes.R, Value = code });

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
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong model");
        }

        [HttpPost]
        public ActionResult DeleteInheritedCode([Bind(Include = "Id,CountryId,R,Value")] Code code)
        {
            int id = -code.Id;
            Code rootCode = _db.Codes.Find(id);
            if (rootCode == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Root code not found");
            for (int i = 0; i < 10; i++)
            {
                if (code.Value[code.Value.Length - 1] == i.ToString()[0])
                    continue;

                _db.Codes.Add(new Code()
                {
                    CountryId = code.CountryId,
                    NetworkId = rootCode.NetworkId,
                    R = code.R,
                    Value = code.Value.Remove(code.Value.Length - 1) + i
                });
            }
            var c = _db.Codes.Remove(rootCode);
            if (c != null)
            {
                _db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Root code not deleted");
        }

        [HttpPost]
        public ActionResult Delete(int?[] ids)
        {
            ushort _deletedCodes = 0;
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

                if (_db.Codes.Remove(code) != null)
                    ++_deletedCodes;
            }
            if (_deletedCodes > 0)
            {
                _db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nothing to remove");
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
