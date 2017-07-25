using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMethods
{
    public class Vectorr
    {

        public static void outputVec(double[] v, int size)
        {
            for (int i = 0; i < size; i++)
                System.Console.Out.Write(Math.Round(v[i], 5) + " ");
            System.Console.Out.Write("\n" + "\n");
        }
    }
}
