using BO;
using Module5TP2_Part1.Models;
using Module5TP2_Part1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Module5TP2_Part1.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            ViewBag.nbPizza = FakeDbPizza.Instance.ListPizza.Count();
            return View(FakeDbPizza.Instance.ListPizza);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateEditVM pvm = new PizzaCreateEditVM();

            pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
            pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

            return View(pvm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateEditVM pvm)
        {
            try
            {
                string errorMessage = ValidatePizza(pvm);

                if (errorMessage == "")
                {
                    Pizza pizza = pvm.Pizza;

                    //gestion de l'id de la pizza a créer
                    if(FakeDbPizza.Instance.ListPizza.Count() == 0) //si c'est la première pizza dans la liste, l'id est 0
                    {
                        pizza.Id = 0;
                    }
                    else //si il y a deja des pizzas, on récupère l'id max et on l'incrémente
                    {
                        int maxId = FakeDbPizza.Instance.ListPizza.Max(p => p.Id);
                        pizza.Id = maxId +1;
                    }
                    

                    //on récupère la pate et ingrédients selon les ids retournés
                    pizza.Pate = FakeDbPizza.Instance.PatesAvailable.FirstOrDefault(p => p.Id == pvm.idSelectedPate);

                    foreach (var idIngredient in pvm.idSelectedIngredients)
                    {
                        pizza.Ingredients.Add(FakeDbPizza.Instance.IngredientsAvailable.FirstOrDefault(i => i.Id == idIngredient));
                    }

                    FakeDbPizza.Instance.ListPizza.Add(pizza);

                    return RedirectToAction("Index");

                } else
                {
                    //envoi de l'erreur à la vue
                    pvm.error = errorMessage;

                    pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                    pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                    return View(pvm);
                }
            }
            catch
            {
                pvm.error = "Une erreur est survenue, merci de soumettre à nouveau le formulaire";

                pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });
                return View(pvm);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaCreateEditVM pvm = new PizzaCreateEditVM();

            pvm.Pizza = FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == id);

            //récupération de la pate et ingredients de la pizza à editer
            pvm.idSelectedPate = pvm.Pizza.Pate.Id;
            pvm.idSelectedIngredients = pvm.Pizza.Ingredients.Select(p => p.Id).ToList();

            pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
            pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

            return View(pvm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateEditVM pvm)
        {
            try
            {
                string errorMessage = ValidatePizza(pvm);

                if (errorMessage == "")
                {
                    Pizza pizzaToEdit = FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == pvm.Pizza.Id);

                    pizzaToEdit.Nom = pvm.Pizza.Nom;

                    Pate pate = FakeDbPizza.Instance.PatesAvailable.FirstOrDefault(p => p.Id == pvm.idSelectedPate);
                    pizzaToEdit.Pate = pate;

                    pizzaToEdit.Ingredients = new List<Ingredient>();
                    foreach (var idIngredient in pvm.idSelectedIngredients)
                    {
                        pizzaToEdit.Ingredients.Add(FakeDbPizza.Instance.IngredientsAvailable.FirstOrDefault(i => i.Id == idIngredient));
                    }

                    return RedirectToAction("Index");

                }
                else
                {
                    //envoi de l'erreur à la vue
                    pvm.error = errorMessage;

                    pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                    pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                    return View(pvm);
                }

            }
            catch
            {
                pvm.error = "Une erreur est survenue, merci de soumettre à nouveau le formulaire";

                pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                return View(pvm);
            }
        }


        //vérifier que la pizza saisie est valide(le nom de la pizza est non null et au moins 1 ingrédient est renseigné)
        public string ValidatePizza(PizzaCreateEditVM pvm)
        {
            string errorMessage = "";

            if (pvm.Pizza.Nom == null)
            {
                errorMessage += "Merci de renseigner le champ Nom. ";

            }

            if (pvm.idSelectedIngredients == null)
            {
                errorMessage += "Merci de selectionner au moins un ingrédient. ";
            }

            return errorMessage;
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FakeDbPizza.Instance.ListPizza.Remove(FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
