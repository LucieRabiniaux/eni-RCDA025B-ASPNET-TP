using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BO
{
    public class Samourai : DbClassWithId
    {
        [Required]
        public int Force { get; set; }
        [Required]
        public string Nom { get; set; }

        //virtual -> permet le lazy loading
        public virtual Arme Arme { get; set; }

        //"Arme" = nom de la propriété Arme ci-dessous
        //[ForeignKey("Arme")] OU [Required] (nb : conseillé d'utiliser ForeignKey)
        //public int idArme { get; set; }

        [DisplayName("Arts martiaux maitrisés")]
        public virtual List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();


        private int potentiel;

        public int Potentiel
        {
            get {
                int armeDegats = (Arme == null) ? 0 : Arme.Degats;
                potentiel = (Force + armeDegats) * (ArtMartials.Count + 1);
                return potentiel; }
        }

    }
}
