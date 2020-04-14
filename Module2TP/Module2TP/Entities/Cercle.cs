using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2TP.Entities
{
    public class Cercle : Forme
    {
        public int Rayon { get; set; }

        //override des méthodes abstraites de Forme (classe mère)

        public override double getAire()
        {
            return Math.PI * getSquare(this.Rayon);
        }
        
        public override double getPerimetre()
        {
            return (2 * Math.PI * this.Rayon);
        }

        public override string ToString()
        {
            return $"Cercle de rayon {this.Rayon}\n" +
                $"Aire = {this.getAire()}\n" +
                $"Périmètre = {this.getPerimetre()}\n";
        }
    }
}
