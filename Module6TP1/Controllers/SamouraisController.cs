using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
            svm.Armes = getArmesAvailable().Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            svm.ArtMartials = db.ArtMartials.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });

            return View(svm);
        }

        // POST: Samourais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiCreateEditVM svm)
        {
            if (ModelState.IsValid && ArmeNotBelongToMoreThanOneSamourai(svm)) //Vérifier qu'une arme ne peut appartenir qu'à un seul samourai
            {
                Samourai samourai = new Samourai();
                samourai.Nom = svm.Samourai.Nom;
                samourai.Force = svm.Samourai.Force;

                //on vérifie si une arme est sélectionnée pour le samourai
                if (svm.IdSelectedArme != null)
                {
                    samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == svm.IdSelectedArme);
                }

                //on vérifie si des arts martiaux sont associés au samourai
                if(svm.IdSelectedArtMartials.Count > 0)
                {
                    samourai.ArtMartials = db.ArtMartials.Where(a => svm.IdSelectedArtMartials.Contains(a.Id)).ToList();
                }

                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //si ModelState non valide, on renvoie la vue Create avec la liste d'armes et d'arts martiaux
            svm.ArtMartials = db.ArtMartials.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            svm.Armes = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            return View(svm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id, bool? emptyArtMartials)
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
            svm.ArtMartials = db.ArtMartials.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });

            List<Arme> armesAvailable = getArmesAvailable();
            //on ajoute au choix des armes dispos celle couramment utilisée par le samourai édité
            if (samouraiDb.Arme != null)
            {
                armesAvailable.Add(db.Armes.FirstOrDefault(a => a.Id == samouraiDb.Arme.Id));
                svm.IdSelectedArme = samouraiDb.Arme.Id;
            }

            svm.Armes = armesAvailable.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });

            //utiliser pour pouvoir supprimer la liste d'arts martiaux sélectionnés (action possible depuis la vue)
            if (emptyArtMartials.HasValue)
            {
                svm.IdSelectedArtMartials = new List<int>();
            } else
            {
                svm.IdSelectedArtMartials = samouraiDb.ArtMartials.Select(a => a.Id).ToList();
            }
            

            return View(svm);
        }

        // POST: Samourais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiCreateEditVM svm)
        {
            if (ModelState.IsValid && ArmeNotBelongToMoreThanOneSamourai(svm))
            {
                #region Explications Lazy / Eager Loading
                //Find() seul -> charge le samourai en lazy loading (type de chargement par défaut).
                //Attention donc ! Tant que l'on n'apelle pas son arme, celle-ci n'est pas chargée en mémoire.
                //Comme l'arme n'est pas chargée, on ne peut pas la modifiée ou la setter à null

                //Samourai samouraiDb = db.Samourais.Find(svm.Samourai.Id);


                //Eager Loading : ajout de .Include()
                //.Include(s => s.Armes) permet de récupérer l'arme en eager
                //Pour la liste des samourais récupérés on demande de charger également l'arme associée.
                #endregion

                Samourai samouraiDb = db.Samourais.Include(s => s.Arme).Include(s => s.ArtMartials).FirstOrDefault(x => x.Id == svm.Samourai.Id);

                samouraiDb.Nom = svm.Samourai.Nom;
                samouraiDb.Force = svm.Samourai.Force;

                samouraiDb.Arme = (svm.IdSelectedArme != null) ? db.Armes.FirstOrDefault(a => a.Id == svm.IdSelectedArme) : null;

                //on vérifie si des arts martiaux sont associés au samourai
                if (svm.IdSelectedArtMartials.Count > 0)
                {
                    samouraiDb.ArtMartials = db.ArtMartials.Where(a => svm.IdSelectedArtMartials.Contains(a.Id)).ToList();
                } else
                {
                    samouraiDb.ArtMartials = new List<ArtMartial>();
                }

                db.Entry(samouraiDb).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //si ModelState non valide, on renvoie la vue Create avec la liste d'armes et d'arts martiaux
            svm.ArtMartials = db.ArtMartials.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            svm.Armes = db.Armes.Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            return View(svm);
        }

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

        //Retourne les armes disponibles (celles non attachées à un samourai)
        public List<Arme> getArmesAvailable()
        {
            List<Arme> armesAvailable = new List<Arme>();
            foreach (var arme in db.Armes.ToList())
            {
                //si aucun samourai ne possède cette arme, on l'ajoute à la liste des armes dispos
                if (!db.Samourais.Any(s => s.Arme.Id == arme.Id))
                {
                    armesAvailable.Add(arme);
                }
            }
            return armesAvailable;
        }

        public bool ArmeNotBelongToMoreThanOneSamourai(SamouraiCreateEditVM svm)
        {
            int? idSelectedArme = svm.IdSelectedArme;
            bool isValid = true;

            //on vérifie si une arme est sélectionnée pour le samourai
            //si oui, on vérifie qu'elle n'appartient pas déjà à un autre samourai
            if (idSelectedArme != null)
            {
                Arme selectedArme = db.Armes.FirstOrDefault(a => a.Id == idSelectedArme);
                if (db.Samourais.Any(s => s.Arme.Id == selectedArme.Id && s.Id != svm.Samourai.Id))
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                ModelState.AddModelError("IdSelectedArme", "Cette arme appartient déjà à un autre samourai");
            }

            return isValid;
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
