using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO.Entities
{
    public abstract class IDbEntity
    {
        [Key]
        public long? Id { get; set; }
    }
}
