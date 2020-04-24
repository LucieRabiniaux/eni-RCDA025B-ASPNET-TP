using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPOgameLike.Data;
using TPOgameLike_BO.Entities;

namespace TPOgameLike.Controllers
{
    public class SolarSystemsController : Controller
    {
        private TPOgameLikeContext db = new TPOgameLikeContext();

        // GET: SolarSystems
        public ActionResult Index()
        {
            return View(db.SolarSystems.ToList());
        }

        // GET: SolarSystems/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarSystem solarSystem = db.SolarSystems.Find(id);
            if (solarSystem == null)
            {
                return HttpNotFound();
            }
            return View(solarSystem);
        }

        // GET: SolarSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolarSystems/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SolarSystem solarSystem)
        {
            if (ModelState.IsValid)
            {
                db.SolarSystems.Add(solarSystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(solarSystem);
        }

        // GET: SolarSystems/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarSystem solarSystem = db.SolarSystems.Find(id);
            if (solarSystem == null)
            {
                return HttpNotFound();
            }
            return View(solarSystem);
        }

        // POST: SolarSystems/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SolarSystem solarSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solarSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solarSystem);
        }

        // GET: SolarSystems/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolarSystem solarSystem = db.SolarSystems.Find(id);
            if (solarSystem == null)
            {
                return HttpNotFound();
            }
            return View(solarSystem);
        }

        // POST: SolarSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SolarSystem solarSystem = db.SolarSystems.Find(id);
            db.SolarSystems.Remove(solarSystem);
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
