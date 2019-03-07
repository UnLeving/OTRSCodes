using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using otrsCode_netCore.Models;
using System.Collections.Generic;

namespace otrsCode_netCore.Controllers
{
    public class AdminController : Controller
    {
        ModelContext db;
        public AdminController(ModelContext context)
        {
            db = context;
        }

        public ActionResult Admin()
        {
            List<Country> countries = new List<Country>();
            foreach (var country in db.Countries)
            {
                countries.Add(new Country() { Id = country.Id, Name = $"{country.Code} {country.Name}" });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Hex");

            return View();
        }
    }
}