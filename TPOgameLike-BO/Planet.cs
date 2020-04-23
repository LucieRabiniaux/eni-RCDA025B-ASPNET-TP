﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TPOgameLike_BO
{
    public class Planet : IDbEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être positive")]
        public int? CaseNb { get; set; }

        //[MinAndMaxElements(2, 5, ErrorMessage = "Une planète ne peux pas avoir plus de 4 ressources")]
        public virtual List<Resource> Resources { get; set; } = new List<Resource>();

        public virtual List<Building> Buildings { get; set; } = new List<Building>();
    }
}