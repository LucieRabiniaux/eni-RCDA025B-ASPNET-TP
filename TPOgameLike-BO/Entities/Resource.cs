using System;
using System.ComponentModel.DataAnnotations;
using TPOgameLike_BO.Validation;

namespace TPOgameLike_BO.Entities
{
    public class Resource : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        [OnlyAuthorizedResourcesNames] //vérifier que par planète on a exactement les noms de ressources précisés dans l'enum
        public string Name { get; set; }

        //TODO : afin d’associer les différentes valeurs une annotation et une méthode d’extension doit être utilisé.

        [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être positive")]
        public int? LastQuantity { get; set; }

        //on utilise une propfull pour pouvoir ajouter des règles de validation dans les setters
        private DateTime lastUpdate;

        [LowerThanCurrentDate(ErrorMessage = "La date doit être inférieure à la date actuelle")]
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set {
                if (IsLowerThanCurrentDate(value))
                {
                    lastUpdate = value;
                }
                else
                {
                    throw new Exception("La date doit être inférieure à la date actuelle");
                }
            }
        }


        //règles de validation utilisées dans les setters
        public bool IsLowerThanCurrentDate(DateTime value)
        {
            bool isValid = true;
            if (value > DateTime.Now)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}