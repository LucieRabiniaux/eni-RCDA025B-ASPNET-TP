using BO;
using Module5TP2_Part1.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Module5TP2_Part1.Validation
{
    //Vérifier que 2 pizzas ou plus n'ont pas la même liste d'ingrédients
    public class UniqueListOfIngredientsAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {

            var list = value as List<int>;

            bool isDifferent = true;

            //comparer si la liste d'ingrédients de la pizza créée ou éditée est la même que celle d'une pizza existante
            foreach (var pizza in FakeDbPizza.Instance.ListPizza)
            {
                //comparer si le nb d'ingrédients est différent
                if (list.Count() != pizza.Ingredients.Count())
                {
                    return isDifferent;
                }
                else
                {
                    if (list.SequenceEqual(pizza.Ingredients.Select(i => i.Id)))
                    {
                        //pizza.Id != 
                        isDifferent = false;
                    }
                }

            }

            return isDifferent;

        }

    }
}