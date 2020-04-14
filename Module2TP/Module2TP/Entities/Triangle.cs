using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2TP.Entities
{
    public class Triangle : Forme
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public override double getAire()
        {
            double demiPerimetre = this.getPerimetre() * 0.5;
            double aire = Math.Sqrt(demiPerimetre*(demiPerimetre - this.A)*(demiPerimetre - this.B)*(demiPerimetre - this.C));
            return aire;
        }

        public override double getPerimetre()
        {

            return this.A + this.B + this.C;
        }

        public override string ToString()
        {
            return $"Triangle de côté A={this.A}, B={this.B}, C={this.C}\n" +
                $"Aire = {this.getAire()}\n" +
                $"Périmètre = {this.getPerimetre()}\n";
        }

    }
}
