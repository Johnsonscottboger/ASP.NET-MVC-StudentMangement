using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Helpers
{
    public static class FloatToScore
    {
        public static float ToScore(this float f)
        {
            double r = f % 1;
            if (r < 0.5) { r = 0; }
            else { r = 0.5; }
            return (float)((long)f + r);
        }
        public static double ToScore(this double d)
        {
            double r = d % 1;
            if (r < 0.5) { r = 0; }
            else { r = 0.5; }
            return (float)((long)d + r);
        }
    }
}