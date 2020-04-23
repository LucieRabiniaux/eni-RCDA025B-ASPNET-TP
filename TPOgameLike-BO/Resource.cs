using System;
using System.ComponentModel.DataAnnotations;
using TPOgameLike_BO.Validation;

namespace TPOgameLike_BO
{
    public class Resource : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        //TODO : vérifier que par planète on aura exactement les noms de ressources suivants :
        //TODO : afin d’associer les différentes valeurs une annotation et une méthode d’extension doit être utilisé.
        public enum NameAuthorized
        {
            énergie,
            oxygène,
            acier,
            uranium
        }

        [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être positive")]
        public int? LastQuantity { get; set; }


        //on utilise une propfull pour pouvoir ajouter des règles de validation dans les setters
        private DateTime lastUpdate;

        [LowerThanCurrentDate(ErrorMessage = "La date dot être inférieure à la date actuelle")]
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
                    throw new Exception("La date dot être inférieure à la date actuelle");
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