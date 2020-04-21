using System.ComponentModel.DataAnnotations;

namespace BO
{
    public class Samourai
    {
        public int Id { get; set; }
        [Required]
        public int Force { get; set; }
        [Required]
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; }
    }
}
