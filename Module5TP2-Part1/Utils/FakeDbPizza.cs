using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module5TP2_Part1.Utils
{
    public class FakeDbPizza
    {
        private static FakeDbPizza _instance;
        static readonly object instanceLock = new object();

        //propfull
        private List<Ingredient> ingredientsAvailable;

        public List<Ingredient> IngredientsAvailable
        {
            get { return ingredientsAvailable; }
        }

        private List<Pate> patesAvailable;

        public List<Pate> PatesAvailable
        {
            get { return patesAvailable; }
        }

        private List<Pizza> listPizza;

        public List<Pizza> ListPizza
        {
            get { return listPizza; }
            set { listPizza = value; }
        }



        //constructeur privé
        private FakeDbPizza()
        {
            ingredientsAvailable = this.GetListIngredientsAvailable();
            patesAvailable = this.GetListPatesAvailable();
            listPizza = new List<Pizza>();
        }

        public static FakeDbPizza Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDbPizza();
                    }
                }
                return _instance;
            }
        }

        private List<Ingredient> GetListIngredientsAvailable()
        {
            return new List<Ingredient>
            {
                new Ingredient{Id=1,Nom="Mozzarella"},
                new Ingredient{Id=2,Nom="Jambon"},
                new Ingredient{Id=3,Nom="Tomate"},
                new Ingredient{Id=4,Nom="Oignon"},
                new Ingredient{Id=5,Nom="Cheddar"},
                new Ingredient{Id=6,Nom="Saumon"},
                new Ingredient{Id=7,Nom="Champignon"},
                new Ingredient{Id=8,Nom="Poulet"}
            };
        }

        private List<Pate> GetListPatesAvailable()
        {
            return new List<Pate>
            {
                new Pate{ Id=1,Nom="Pate fine, base crême"},
                new Pate{ Id=2,Nom="Pate fine, base tomate"},
                new Pate{ Id=3,Nom="Pate épaisse, base crême"},
                new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
            };
        }

    }
}