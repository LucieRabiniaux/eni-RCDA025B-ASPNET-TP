using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO.Validation
{
    //vérifier si le nom de ressource donné est bien présent dans l'énum des noms authorisés (oxygène...)
    class OnlyAuthorizedResourcesNamesAttribute : ValidationAttribute
    {
        
        public override bool IsValid(object value)
        {
            bool isValid = true;

            if(!Enum.IsDefined(typeof(ResourcesNamesEnum), value))
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
