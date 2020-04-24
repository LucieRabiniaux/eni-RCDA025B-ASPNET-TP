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
    public class PlanetsController : Controller
    {
        private TPOgameLikeContext db = new TPOgameLikeContext();

        // GET: Planets
        public ActionResult Index()
        {
            return View(db.Planets.ToList());
        }

        // GET: Planets/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planet planet = db.Planets.Find(id);
            if (planet == null)
            {
                return HttpNotFound();
            }
            return View(planet);
        }

        // GET: Planets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CaseNb")] Planet planet)
        {
            if (ModelState.IsValid)
            {
                db.Planets.Add(planet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planet);
        }

        // GET: Planets/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planet planet = db.Planets.Find(id);
            if (planet == null)
            {
                return HttpNotFound();
            }
            return View(planet);
        }

        // POST: Planets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CaseNb")] Planet planet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planet);
        }

        // GET: Planets/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planet planet = db.Planets.Find(id);
            if (planet == null)
            {
                return HttpNotFound();
            }
            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Planet planet = db.Planets.Find(id);
            db.Planets.Remove(planet);
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
