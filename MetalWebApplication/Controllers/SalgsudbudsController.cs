using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetalWebApplication.Models;

namespace MetalWebApplication.Controllers
{
    public class SalgsudbudsController : Controller
    {
        private MetalauktionEntities db = new MetalauktionEntities();

        // GET: Salgsudbuds
        public ActionResult Index()
        {
            return View(db.Salgsudbud.ToList().FindAll(x => x.Tidsfrist > DateTime.Now));
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
