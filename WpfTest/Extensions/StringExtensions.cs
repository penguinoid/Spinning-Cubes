using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Extensions
{
    public static class StringExtensions
    {
        public static string ToNameFormat(this int ordinal)
        {
            var stringOrdinal = ordinal.ToString();
            return stringOrdinal.StartsWith("-") 
                ? string.Concat("m", stringOrdinal.Substring(1, stringOrdinal.Length - 1)) 
                : string.Concat("p", stringOrdinal);
        }

        public static int ToAbsoluteColor(this int color)
        {
            return color < 0 ? color * -1 : color;
        }
    }
}
