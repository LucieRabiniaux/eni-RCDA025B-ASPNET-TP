using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2TP.Entities
{
    public class Carre : Forme
    {
        public int Longueur { get; set; }

        public override double getAire()
        {
            return this.Longueur * this.Longueur;
        }

        public override double getPerimetre()
        {
            return this.Longueur * 4;
        }

        public override string ToString()
        {
            return $"Carré de côté {this.Longueur}\n" +
                $"Aire = {this.getAire()}\n" +
                $"Périmètre = {this.getPerimetre()}\n";
        }


    }
}
