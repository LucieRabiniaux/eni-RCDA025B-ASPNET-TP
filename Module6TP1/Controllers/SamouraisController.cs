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
using Module6TP1.Models;

namespace Module6TP1.Controllers
{
    public class SamouraisController : Controller
    {
        private Module6TP1Context db = new Module6TP1Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiCreateEditVM svm = new SamouraiCreateEditVM();
            //on récupère les armes en bdd pour alimenter la dropdown sur la vue
            svm.Arme = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });

            return View(svm);
        }

        // POST: Samourais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiCreateEditVM svm)
        {
            if (ModelState.IsValid)
            {
                Samourai samourai = new Samourai();
                if(svm.Samourai.Nom != null)
                {
                    samourai.Nom = svm.Samourai.Nom;
                } else
                {
                    ModelState.AddModelError("Samourai.Nom", "Deux pizzas ne peuvent pas avoir la même liste d'ingrédients.");
                }
                samourai.Force = svm.Samourai.Force;
                //on vérifie si une arme est associée au samourai
                if(svm.IdSelectedArme != null)
                {
                    samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == svm.IdSelectedArme);
                }
                

                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(svm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SamouraiCreateEditVM svm = new SamouraiCreateEditVM();

            Samourai samouraiDb = db.Samourais.Find(id);
            if (samouraiDb == null)
            {
                return HttpNotFound();
            }
            svm.Samourai = samouraiDb;
            svm.Arme = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            if(samouraiDb.Arme != null)
            {
                svm.IdSelectedArme = samouraiDb.Arme.Id;
            }
            
            return View(svm);
        }

        // POST: Samourais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiCreateEditVM svm)
        {
            if (ModelState.IsValid)
            {
                //Samourai samouraiDb = db.Samourais.Find(svm.Samourai.Id);

                //Include(s => s.Arme) permet de récupérer l'arme pour pouvoir ensuite la passer à null si besoin (contourner le lazy loading par défaut)
                Samourai samouraiDb = db.Samourais.Include(s => s.Arme).FirstOrDefault(x => x.Id == svm.Samourai.Id);

                samouraiDb.Nom = svm.Samourai.Nom;
                samouraiDb.Force = svm.Samourai.Force;

                //if(svm.IdSelectedArme != null)
                //{
                //    Arme arme = db.Armes.FirstOrDefault(a => a.Id == svm.IdSelectedArme);
                //    samouraiDb.Arme = arme;
                //} else
                //{
                //    samouraiDb.Arme = null;
                //}

                samouraiDb.Arme = (svm.IdSelectedArme != null) ? db.Armes.FirstOrDefault(a => a.Id == svm.IdSelectedArme) : null;

                db.Entry(samouraiDb).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            //si ModelState non valide, on renvoie la vue Edit avec la liste d'armes
            svm.Arme = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            return View(svm);
        }

        //public bool

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
