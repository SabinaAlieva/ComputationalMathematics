using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class function
    {
        public double integralMiddleRectangle(double lim1, double lim2, double step)
        {
            double segment = (lim2 - lim1) / step;
            double sum = 0;

            while (lim1 < lim2)
            {
                sum += segment * F(lim1 + segment / 2);
                lim1 += segment;
            }
            return sum;
        }

        public double integralMoment(double lim1, double lim2, int power)
        {
            double sum = 0;
            switch (power)
            {
                case 0: sum = integralX0(lim1, lim2);
                    break;
                case 1: sum = integralX1(lim1, lim2);
                    break;
                case 2: sum = integralX2(lim1, lim2);
                    break;
                case 3: sum = integralX3(lim1, lim2);
                    break;
                case 4: sum = integralX4(lim1, lim2);
                    break;
                case 5: sum = integralX5(lim1, lim2);
                    break;
                
            }
            return sum;
        }

        public double f(double x)
        {
            double result;
            result = (2.5 * Math.Cos(2 * x) * Math.Exp(2.0 / 3.0 * x) + 4 * Math.Sin(3.5 * x) * Math.Exp(-3 * x) + 3 * x);
            return result;
        }

        public double F(double x)
        {
            double result;
            result = (2.5 * Math.Cos(2 * x) * Math.Exp(2.0 / 3.0 * x) + 4 * Math.Sin(3.5 * x) * Math.Exp(-3 * x) + 3 * x) / Math.Pow(x - 0.1, 0.2);
            return result;
        }

        public double integralX0(double lim1, double lim2)
        {
            double sum;
            sum = 1.25 * (Math.Pow(lim2 - 0.1, 0.8) - Math.Pow(lim1 - 0.1, 0.8));
            return sum;
        }

        public double integralX1(double lim1, double lim2)
        {
            double sum;
            sum = Math.Pow(5, 0.2) * 1.0 / 72.0 * (Math.Pow(5 * lim2 - 0.5, 0.8) * (8 * lim2 + 1) - Math.Pow(5 * lim1 - 0.5, 0.8) * (8 * lim1 + 1));
            return sum;
        }

        public double integralX2(double lim1, double lim2)
        {
            double sum;
            sum = Math.Pow(5, 0.2) * (Math.Pow(5 * lim2 - 0.5, 0.8) * (72 * lim2 * lim2 + 8 * lim2 + 1) - Math.Pow(5 * lim1 - 0.5, 0.8) * (72 * lim1 * lim1 + 8 * lim1 + 1)) / 1008.0;
            return sum;
        }

        public double integralX3(double lim1, double lim2)
        {
            double sum;
            sum = Math.Pow(5, 0.2) * (Math.Pow(5 * lim2 - 0.5, 0.8) * (672 * Math.Pow(lim2, 3) + 72 * lim2 * lim2 + 8 * lim2 + 1) - Math.Pow(5 * lim1 - 0.5, 0.8) * (672 * Math.Pow(lim1, 3) +  72 * lim1 * lim1 + 8 * lim1 + 1)) / 12768.0;
            return sum;
        }

        public double integralX4(double lim1, double lim2)
        {
            double sum;
            sum = Math.Pow(5, 0.2) * (Math.Pow(5 * lim2 - 0.5, 0.8) * (6384 * Math.Pow(lim2, 4) + 672 * Math.Pow(lim2, 3) + 72 * lim2 * lim2 + 8 * lim2 + 1) - Math.Pow(5 * lim1 - 0.5, 0.8) * (6384 * Math.Pow(lim1, 4) + 672 * Math.Pow(lim1, 3) + 72 * lim1 * lim1 + 8 * lim1 + 1)) / 153216.0; ;
            return sum;
        }

        public double integralX5(double lim1, double lim2)
        {
            double sum;
            sum = Math.Pow(5, 0.2) * (Math.Pow(5 * lim2 - 0.5, 0.8) * (306432 * Math.Pow(lim2, 5) + 31920 * Math.Pow(lim2, 4) + 3360 * Math.Pow(lim2, 3) + 360 * lim2 * lim2 + 40 * lim2 + 5) - Math.Pow(5 * lim1 - 0.5, 0.8) * (306432 * Math.Pow(lim1, 5) + 31920 * Math.Pow(lim1, 4) + 3360 * Math.Pow(lim1, 3) + 360 * lim1 * lim1 + 40 * lim1 + 5)) / 8886528.0;
            return sum;
        }

        public double[] kardano(double[] a, double[] x)
        {
             double current = a[0]; a[0] = a[2]; a[2] = current;
             double p = a[1] - a[0] * a[0] / 3.0; // p = b - a^2/3
             double q = a[2] + 2.0 * a[0] * a[0] * a[0] / 27.0 - a[0] * a[1] / 3.0; // q = c + 2a^3/27 - ab/3
             double determinant = q * q / 4.0 + p * p * p / 27.0;
             
             if (determinant < 0)
             {
                 double fi = 0;
                 if (q < 0) fi = Math.Atan(2.0 * Math.Sqrt(-determinant) / (-q));
                 if (q > 0) fi = Math.Atan(2.0 * Math.Sqrt(-determinant) / (-q) + Math.PI);
                 if (q == 0) fi = Math.PI / 2.0;

                 x[0] = 2.0 * Math.Sqrt(-p / 3.0) * Math.Cos(fi / 3.0) - a[0] / 3.0;
                 x[1] = 2.0 * Math.Sqrt(-p / 3.0) * Math.Cos(fi / 3.0 + 2.0 * Math.PI / 3.0) - a[0] / 3.0;
                 x[2] = 2.0 * Math.Sqrt(-p / 3.0) * Math.Cos(fi / 3.0 + 4.0 * Math.PI / 3.0) - a[0] / 3.0;
             }
             if (determinant > 0)
             {
                x[1] = 0;
                if ((-q) / 2.0 + Math.Pow(determinant, 1.0 / 2.0) < 0)
                    x[1] += -Math.Pow((q) / 2.0 - Math.Pow(determinant, 1.0 / 2.0), 1.0 / 3.0);
                else x[1] += Math.Pow((-q) / 2.0 + Math.Pow(determinant, 1.0 / 2.0), 1.0 / 3.0);
                if (-q / 2.0 - Math.Pow(determinant, 1.0 / 2.0) < 0)
                    x[1] += -Math.Pow(q / 2.0 + Math.Pow(determinant, 1.0 / 2.0), 1.0 / 3.0) - a[0] / 3.0;
                else x[1] += Math.Pow(-q / 2.0 - Math.Pow(determinant, 1.0 / 2.0), 1.0 / 3.0) - a[0] / 3.0;
             }
             if (determinant == 0){
                 x[0] = 2 * Math.Pow(-q / 2.0, 1.0 / 3.0) - a[0] / 3.0;
                 x[1] =  -Math.Pow(-q / 2.0, 1.0 / 3.0) - a[0] / 3.0;
             }
            return x;
        }
    }
}
