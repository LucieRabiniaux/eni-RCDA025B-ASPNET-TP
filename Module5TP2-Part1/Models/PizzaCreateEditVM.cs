using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2_Part1.Models
{
    public class PizzaCreateEditVM
    {

        public Pizza Pizza { get; set; }

        public IEnumerable<SelectListItem> Pate { get; set; }

        public IEnumerable<SelectListItem> Ingredients { get; set; }

        //NB : si on integre une selectbox ou un dropdown dans une vue, il faut définir aussi des propriétés qui vont stocker les valeurs retournées ces inputs (attribut value de l'input)
        //DropdownlistFor -> choix unique donc simple int
        //ListboxFor -> choix multiples donc list<int>
        public int idSelectedPate { get; set; } //stocker l'id de la pate selectionnée (sur la vue create et edit)
        public List<int> idSelectedIngredients { get; set; } //stocker les ids des ingrédients sélectionnés (sur la vue create ou edit)

        //affichage des erreurs
        public string error;

    }
}