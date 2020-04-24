using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPOgameLike_BO.Entities;

namespace TPOgameLike_BO.Entities
{
    public class SolarSystem : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        public virtual List<Planet> Planets { get; set; }
    }
}
