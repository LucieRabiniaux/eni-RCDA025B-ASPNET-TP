using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO.Entities
{
    public abstract class ResourceGenerator : Building
    {

		public virtual List<Resource> ResourceBySecond
		{
			get { return new List<Resource>(); }
		}


	}
}
