using MetalWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetalWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                
                if (kunde.Navn != null && kunde.Email != null)
                {
                    
                    TempData["navn"] = kunde.Navn;
                    TempData["mail"] = kunde.Email;
                    return RedirectToAction("Index", "Salgsudbuds");
                }
            }
            return View();
        }
    }
}