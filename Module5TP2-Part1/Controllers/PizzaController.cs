using BO;
using Module5TP2_Part1.Models;
using Module5TP2_Part1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Module5TP2_Part1.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
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

                if (ModelState.IsValid && listIngredientsIsDifferent(pvm))
                {
                    Pizza pizza = pvm.Pizza;

                    //gestion de l'id de la pizza a créer
                    pizza.Id = FakeDbPizza.Instance.ListPizza.Count == 0 ? 1 : FakeDbPizza.Instance.ListPizza.Max(p => p.Id) + 1;

                    //on récupère la pate et ingrédients selon les ids retournés
                    pizza.Pate = FakeDbPizza.Instance.PatesAvailable.FirstOrDefault(p => p.Id == pvm.idSelectedPate);

                    //listIngredientsIsDifferent() -> on vérifie si la pizza n'a pas la même liste d'ingrédients qu'une pizza existante
                    if (pvm.idSelectedIngredients != null)
                    {
                        pizza.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Where(i => pvm.idSelectedIngredients.Contains(i.Id)).ToList();
                    }

                    FakeDbPizza.Instance.ListPizza.Add(pizza);

                    return RedirectToAction("Index");

                }
                else
                {
                    pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                    pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                    return View(pvm);
                }
            }
            catch
            {
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
                if (ModelState.IsValid && listIngredientsIsDifferent(pvm))
                {
                    Pizza pizzaToEdit = FakeDbPizza.Instance.ListPizza.FirstOrDefault(p => p.Id == pvm.Pizza.Id);

                    pizzaToEdit.Nom = pvm.Pizza.Nom;

                    Pate pate = FakeDbPizza.Instance.PatesAvailable.FirstOrDefault(p => p.Id == pvm.idSelectedPate);
                    pizzaToEdit.Pate = pate;

                    pizzaToEdit.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Where(i => pvm.idSelectedIngredients.Contains(i.Id)).ToList();

                    return RedirectToAction("Index");

                }
                else
                {
                    pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                    pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                    return View(pvm);
                }

            }
            catch
            {
                pvm.Pate = FakeDbPizza.Instance.PatesAvailable.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() });
                pvm.Ingredients = FakeDbPizza.Instance.IngredientsAvailable.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() });

                return View(pvm);
            }
        }


        //vérifier que deux pizzas n'ont pas la même liste d ingrédients
        public bool listIngredientsIsDifferent(PizzaCreateEditVM pvm)
        {
            bool isDifferent = true;

            Pizza pizzaToCreateOrEdit = pvm.Pizza;
            var listIngredientsId = pvm.idSelectedIngredients.OrderBy(i => i);

            //comparer si la liste d'ingrédients de la pizza créée ou éditée est la même que celle d'une pizza existante
            foreach (var pizza in FakeDbPizza.Instance.ListPizza)
            {
                //comparer si le nb d'ingrédients est différent
                if (listIngredientsId.Count() != pizza.Ingredients.Count())
                {
                    return isDifferent;
                }
                else
                {
                    if (listIngredientsId.SequenceEqual(pizza.Ingredients.Select(i => i.Id).OrderBy(i => i)))
                    {
                        //on vérifie qu'il ne s'agit pas de la pizza en cours d'edition
                        if (pizzaToCreateOrEdit.Id == pizza.Id)
                        {
                            isDifferent = true;
                        }
                        else
                        {
                            isDifferent = false;
                            break;
                        }
                    }
                }

            }

            if (!isDifferent)
            {
                ModelState.AddModelError("", "Deux pizzas ne peuvent pas avoir la même liste d'ingrédients.");
            }

            return isDifferent;
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
