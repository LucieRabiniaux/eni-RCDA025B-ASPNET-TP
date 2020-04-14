using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2TP.Entities
{
    public abstract class Forme
    {
        public int Aire { get; set; }
        public int Perimetre { get; set; }


        public abstract double getAire();
        public abstract double getPerimetre();

        public double getSquare(int number)
        {
            return number * number;
        }

    }
}
