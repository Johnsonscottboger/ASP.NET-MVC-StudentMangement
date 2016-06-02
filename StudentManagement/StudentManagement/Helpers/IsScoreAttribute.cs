using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Helpers
{
    public class IsScoreAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //如果value有小数,
            if((float)value%1!=0)
            {
                //如果对1求余结果不为0.5或0.0。则不符合分数形式
                if((float)value%1!=0.5||(float)value%1!=0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}