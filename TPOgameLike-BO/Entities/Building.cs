using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPOgameLike_BO.Entities
{
    public abstract class Building : IDbEntity
    {

        private string name;
        private int? level;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être positive")]
        public int? Level
        {
            get { return level; }
            set { level = value; }
        }


        [NotMapped] //NB : privilégier les annotations à la fluent API
        public virtual int? CellNb
        {
            get { return level; }
        }

        //retourne la liste des ressources totale ayant permit la construction du bâtiment
        [NotMapped]
        public virtual List<Resource> TotalCost
        {
            get { return new List<Resource>(); }
        }

        //retourne la liste des ressources permettant la construction du prochain niveau du bâtiment
        [NotMapped]
        public virtual List<Resource> NextCost
        {
            get { return new List<Resource>(); }
        }

    }
}