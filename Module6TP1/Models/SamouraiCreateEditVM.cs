using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Module6TP1.Models
{
    public class SamouraiCreateEditVM
    {
        public Samourai Samourai { get; set; }

        public IEnumerable<SelectListItem> Arme { get; set; } = new List<SelectListItem>();

        public int? IdSelectedArme { get; set; } //Nullable int car un samourai n'a pas forcément une arme associée

    }
}