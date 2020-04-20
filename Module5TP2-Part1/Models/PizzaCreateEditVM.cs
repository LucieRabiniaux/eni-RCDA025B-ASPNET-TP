using BO;
using Module5TP2_Part1.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2_Part1.Models
{
    public class PizzaCreateEditVM
    {

        [UniquePizzaName(ErrorMessage = "Il existe déjà une pizza portant ce nom")]
        public Pizza Pizza { get; set; }

        public IEnumerable<SelectListItem> Pate { get; set; }

        public IEnumerable<SelectListItem> Ingredients { get; set; }

        //NB : si on integre une selectbox ou un dropdown dans une vue, il faut définir aussi des propriétés qui vont stocker les valeurs retournées ces inputs (attribut value de l'input)
        //DropdownlistFor -> choix unique donc simple int
        //ListboxFor -> choix multiples donc list<int>

        [Required(ErrorMessage = "Merci de sélectionner une pâte")]
        public int idSelectedPate { get; set; } //stocker l'id de la pate selectionnée (sur la vue create et edit)

        [MinAndMaxElements(2, 5, ErrorMessage = "Une pizza doit avoir entre 2 et 5 ingrédients")]
        //[UniqueListOfIngredients(ErrorMessage = "Deux pizzas ne peuvent pas avoir la même liste d'ingrédients.")]
        public List<int> idSelectedIngredients { get; set; } = new List<int>(); //stocker les ids des ingrédients sélectionnés (sur la vue create ou edit)

    }
}