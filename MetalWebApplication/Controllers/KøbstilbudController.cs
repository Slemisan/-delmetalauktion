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
    public class KøbstilbudController : Controller
    {
        private MetalauktionEntities db = new MetalauktionEntities();

        // GET: Købstilbud
        public ActionResult Index()
        {
            var købstilbud = db.Købstilbud.Include(k => k.Kunde).Include(k => k.Salgsudbud);
            return View(købstilbud.ToList());
        }

        // GET: Købstilbud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Købstilbud købstilbud = db.Købstilbud.Find(id);
            if (købstilbud == null)
            {
                return HttpNotFound();
            }
            return View(købstilbud);
        }

        // GET: Købstilbud/Create
        public ActionResult Create(String id)
        {
            TempData["udbudId"] = id;
            ViewBag.KundeId = new SelectList(db.Kunde, "Email", "Navn");
            ViewBag.SalgsudbudId = new SelectList(db.Salgsudbud, "Id", "MetalType");
            return View();
        }

        // POST: Købstilbud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pris,SalgsudbudId,KundeId")] Købstilbud købstilbud)
        {
            if (ModelState.IsValid)
            {
                int højesteBud = 0;
                try
                {
                    højesteBud = (int)db.Købstilbud.ToList().FindAll(x => x.SalgsudbudId == Int32.Parse(TempData.Peek("udbudId").ToString())).Max(x => x.Pris);
                }
                catch
                {

                }

                if (købstilbud.Pris > højesteBud)
                {
                    købstilbud.KundeId = (string)TempData["mail"];
                    købstilbud.SalgsudbudId = int.Parse(TempData["udbudId"].ToString());

                    Kunde k = new Kunde();
                    k.Email = købstilbud.KundeId;
                    k.Navn = (string)TempData["navn"];

                    købstilbud.Kunde = k;

                    db.Købstilbud.Add(købstilbud);
                    db.SaveChanges();
                    return RedirectToAction("Success", "Købstilbud");
                } else
                {
                    return RedirectToAction("Failure", "Købstilbud");
                }
            }

            ViewBag.KundeId = new SelectList(db.Kunde, "Email", "Navn", købstilbud.KundeId);
            ViewBag.SalgsudbudId = new SelectList(db.Salgsudbud, "Id", "MetalType", købstilbud.SalgsudbudId);
            return View(købstilbud);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failure()
        {
            return View();
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
