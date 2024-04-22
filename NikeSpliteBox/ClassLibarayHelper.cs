using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikeSpliteBox
{
    public static class ClassLibarayHelper
    {
        public static decimal GetSKULength(this string sku)
        {
            return Convert.ToDecimal(sku.Split(',')[0]);
        }
        public static decimal GetSKUWidth(this string sku)
        {
            return Convert.ToDecimal(sku.Split(',')[1]);
        }
        public static decimal GetSKUHeight(this string sku)
        {
            return Convert.ToDecimal(sku.Split(',')[2]);
        }
    }
}
