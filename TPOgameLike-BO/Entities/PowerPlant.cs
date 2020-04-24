using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO.Entities
{
    public class PowerPlant : ResourceGenerator
    {
        private int? energyCost;
        private int? oxygenCost;
        private int? steelCost;
        private int? uraniumCost;


        public override List<Resource> TotalCost => base.TotalCost;

        public override List<Resource> NextCost
        {
            get
            {

            }
        }

        public override List<Resource> ResourceBySecond => base.ResourceBySecond;



        public int? getEnergyCost(int? level)
        {
            return 1 * level;
        }
        public int? getOxygenCost(int? level)
        {
            return 1 * level + (200 * (level / 10)) + 20;
        }
        public int? getSteelCost(int? level)
        {
            return 1 * level + (100 * (level / 8)) + 20;
        }
        public int? getUraniumCost(int? level)
        {
            return 3 * (level * level * level) + (200 * (level / 20)) + 20;
        }


    }
}
