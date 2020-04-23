using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO
{
    public class SolarSystem : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        public virtual List<Planet> Planets { get; set; }
    }
}
