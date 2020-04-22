using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ArtMartial : DbClassWithId
    {
        [Required]
        public string Nom { get; set; }

    }
}
