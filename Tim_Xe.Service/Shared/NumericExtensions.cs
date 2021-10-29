using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Service.Shared
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
