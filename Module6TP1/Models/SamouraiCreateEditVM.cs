using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Module6TP1.Models
{
    public class SamouraiCreateEditVM
    {
        public Samourai Samourai { get; set; }

        [DisplayName("Arme")]
        
        public IEnumerable<SelectListItem> Armes { get; set; } = new List<SelectListItem>();

        public int? IdSelectedArme { get; set; } // int? = nullable int car un samourai n'a pas forcément une arme associée

        [DisplayName("Arts martiaux maitrisés")]
        public IEnumerable<SelectListItem> ArtMartials { get; set; } = new List<SelectListItem>();

        public List<int> IdSelectedArtMartials { get; set; } = new List<int>();

    }
}