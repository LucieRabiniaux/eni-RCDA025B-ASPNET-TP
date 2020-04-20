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
    //vérifier si l'élement est unique
    public class UniquePizzaNameAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            Pizza pizza = value as Pizza;

            if (FakeDbPizza.Instance.ListPizza.Any(p => p.Nom.ToUpper() == pizza.Nom.ToUpper() && p.Id != pizza.Id))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}