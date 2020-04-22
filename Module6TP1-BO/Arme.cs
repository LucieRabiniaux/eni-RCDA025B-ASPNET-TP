using System.ComponentModel.DataAnnotations;

namespace BO
{
    public class Arme : DbClassWithId
    {
   
        [Required]
        public string Nom { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Merci de saisir une valeur supérieure à 0")]
        public int Degats { get; set; }
    }
}