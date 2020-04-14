using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2TP.Entities
{
    public class Rectangle : Forme
    {
        public int Largeur { get; set; }
        public int Longueur { get; set; }

        public override double getAire()
        {
            return this.Longueur * this.Largeur;
        }

        public override double getPerimetre()
        {
            return (this.Longueur + this.Largeur) * 2;
        }

        public override string ToString()
        {
            return $"Rectangle de longueur {this.Longueur} et de largeur {this.Largeur}\n" +
                $"Aire = {this.getAire()}\n" +
                $"Périmètre = {this.getPerimetre()}\n";
        }

    }
}
