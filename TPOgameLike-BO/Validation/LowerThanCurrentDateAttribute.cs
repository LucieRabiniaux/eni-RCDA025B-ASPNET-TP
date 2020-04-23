using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPOgameLike_BO.Validation
{
    class LowerThanCurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = (DateTime)value;
            bool isValid = true;

            if(dateTime > DateTime.Now)
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
