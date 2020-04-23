using System.ComponentModel.DataAnnotations;

namespace TPOgameLike_BO
{
    public abstract class Building : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être positive")]
        public int? Level { get; set; }
    }
}