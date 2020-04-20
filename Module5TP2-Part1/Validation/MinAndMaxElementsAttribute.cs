using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Module5TP2_Part1.Validation
{
    //vérifier qu'une liste respecte un nombre minimum et maximum d'items
    public class MinAndMaxElementsAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;
        public MinAndMaxElementsAttribute(int minElements, int maxElements)
        {
            _min = minElements;
            _max = maxElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list.Count < _min || list.Count > _max)
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