using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriotNet
{
    public static class Randomizer
    {
        public static double RandNumber()
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            double rnd = rndNum.NextDouble();

            return rnd;
        }
    }
}
