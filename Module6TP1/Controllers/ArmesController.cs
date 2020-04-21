using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Module6TP1.Data;

namespace Module6TP1.Controllers
{
    public class ArmesController : Controller
    {
        private Module6TP1Context db = new Module6TP1Context();

        // GET: Armes
        public ActionResult Index()
        {
            return View(db.Armes.ToList());
        }

        // GET: Armes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // GET: Armes/Create
        public ActionResult Create()
        {
            Arme arme = new Arme();

            return View(arme);
        }

        // POST: Armes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Arme arme)
        {
            if (ModelState.IsValid)
            {
                db.Armes.Add(arme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arme);
        }

        // GET: Armes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // POST: Armes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Degats")] Arme arme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arme);
        }

        // GET: Armes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arme arme = db.Armes.Find(id);
            if (arme == null)
            {
                return HttpNotFound();
            }
            return View(arme);
        }

        // POST: Armes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arme arme = db.Armes.Find(id);

            //avant de supprimer l'arme, on l'enlève à tous les samourais qui l'utilisent
            var listSamouraisDb = db.Samourais.ToList();
            foreach (var samouraiDb in listSamouraisDb)
            {
                if(samouraiDb.Arme != null)
                {
                    if (samouraiDb.Arme.Id == id)
                    {
                        samouraiDb.Arme = null;
                    }
                }

            }

            db.Armes.Remove(arme);
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
