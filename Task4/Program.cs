using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathMethods;
using System.Collections;

namespace Task4
{
    class Program
    {
        static double exactValue, metodicMistake;
        static double limes1 = 0.1;
        static double limes2 = 2.3;
        static double[] m, x, A, a;
        static function f;
        static Matrix M;


        static void Main(string[] args)
        {
            f = new function(); 
            M = new Matrix(3);
            m = new double[3];
            x = new double[3];
            A = new double[3];
            double res, res2, res3;

            // * * * * * * * * * * * * * Точное и приближенное значение * * * * * * * * * * * * * * * * * 

            exactValue = 3.57886;
            writeText("точное значение", exactValue);
            writeText("приближенное вычисление", f.integralMiddleRectangle(limes1, limes2, 1000));

            // * * * * * * * * * * * * * Вариант ньютона - Котса * * * * * * * * * * * *

            res = NewtonKF(limes1, limes2, 1);
            metodicMistake = 0.017416667;

            writeText("вариант Ньютона - Котса", res,  Math.Abs(exactValue - res), metodicMistake);

            //* * * * * * * * * * * * * Cоставная ИКФ с нужной точностью* * * * * * * * * *

            double l1 = limes1;
            double step = (limes2 - limes1) / 10;
            double l2 = 0;
            double error = 10;
            double st = 4;
            double speed = 3;
            double L = 2;

            System.Console.Out.Write("скорость сходимости составной ИКФ " + "\n");

            while(error > 0.000001)
            {
                step *= L;
                l2 = limes1 + step;
                res = NewtonKF(l1, l2, step);
                
                step = step / L;
                l2 = limes1 + step;
                res2 = NewtonKF(l1, l2, step);

                step = step / L ;
                l2 = limes1 + step;
                res3 = NewtonKF(l1, l2, step);

                speed = -Math.Log(Math.Abs((res3 - res2) / (res2 - res))) / Math.Log(L);
                System.Console.Out.Write(speed + "\n");
                
                error = Math.Abs((res2 - res) / (Math.Pow(L, st) - 1)); 
                res += (res2 - res) / (1 - Math.Pow(L, -st));
            }

            writeText("составная ИКФ", res,  Math.Abs(exactValue - res), error);

            //* * * * * * * * * * * * * * * * Cоставная ИКФ c оптимальным шагом * * * * * * * * * *

            double hOpt = Math.Ceiling( (limes2 - limes1) / (step * L * Math.Pow((0.00001 / error), 1.0 / st)));
            hOpt = (limes2 - limes1) / hOpt;
            l1 = limes1;
            l2 = limes1 + hOpt;
            res2 = NewtonKF(l1, l2, hOpt);
            error = Math.Abs((res2 - res) / (Math.Pow(L, st) - 1)); 

            writeText("составная ИКФ с оптимальным шагом", res2, Math.Abs(exactValue - res2), error);

            //* * * * * * * * * * * * * * * * * Форма Гаусса * * * * * * * * * * * * * *
            
            m = new double[6];
            res = GaussKF(limes1, limes2, 1);
            //MathMethods.Vectorr.outputVec(x, 3);
            metodicMistake = 0.00014;
            
            writeText("вариант Гаусса", res, Math.Abs(exactValue - res), metodicMistake);
            
            //* * * * * * * * * * * * * * * Составная форма Гаусса * * * * * * * * * * * * * *
            
            l1 = limes1;
            step = (limes2 - limes1) / 10;
            error = 10;

            System.Console.Out.Write("скорость сходимости составной КФ Гаусса" + "\n");

            while (error > 0.000001)
            {
                step *= L;
                l2 = limes1 + step;
                res = GaussKF(l1, l2, step);

                step = step / L;
                l2 = limes1 + step;
                res2 = GaussKF(l1, l2, step);

                step = step / L ;
                l2 = limes1 + step;
                res3 = GaussKF(l1, l2, step);

                speed = -Math.Log(Math.Abs((res3 - res2) / (res2 - res))) / Math.Log(L);
                System.Console.Out.Write(speed + "\n");
                error = Math.Abs((res2 - res) / (Math.Pow(L, st) - 1));
                res += (res2 - res) / (1 - Math.Pow(L, -st));
            }

            writeText("составная ИКФ по Гауссу", res, Math.Abs(exactValue - res), error);

            //* * * * * * * * * * * * * * * Cоставная форма Гаусса c оптимальным шагом * * * * * * * * * *

            hOpt = Math.Ceiling((limes2 - limes1) / (step * L * Math.Pow((0.00001 / error), 1.0 / st)));
            hOpt = (limes2 - limes1) / hOpt;
            l1 = limes1;
            l2 = limes1 + hOpt;
            res2 = GaussKF(l1, l2, hOpt);
            error = Math.Abs((res2 - res) / (Math.Pow(L, st) - 1));

            writeText("составная ИКФ Гаусса с оптимальным шагом", res2, Math.Abs(exactValue - res2), error);



            System.Console.In.Read();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

            static void writeText(string s, double num)
            {
                System.Console.Out.Write(s + "\n");
                System.Console.Out.Write(num + "\n" + "\n");
            }

            static void writeText(string s, double num, double num2, double num3)
            {
                System.Console.Out.Write("\n" + s + "\n");
                System.Console.Out.Write(num + "\n");
                System.Console.Out.Write("точная погрешность" + "\n");
                System.Console.Out.Write(num2 + "\n");
                System.Console.Out.Write("методическая погрешность" + "\n");
                System.Console.Out.Write(num3 + "\n" + "\n");
            }

            static double NewtonKF(double l1, double l2, double step)
            {
                double result = 0;
                while (l2 <= limes2)
                {
                    m[0] = f.integralMoment(l1, l2, 0);
                    m[1] = f.integralMoment(l1, l2, 1);
                    m[2] = f.integralMoment(l1, l2, 2);

                    x[0] = l1; x[1] = l1 + (l2 - l1) / 2; x[2] = l2;

                    M.matrix[0, 0] = 1;            M.matrix[0, 1] = 1;           M.matrix[0, 2] = 1;
                    M.matrix[1, 0] = x[0];         M.matrix[1, 1] = x[1];        M.matrix[1, 2] = x[2];
                    M.matrix[2, 0] = x[0] * x[0];  M.matrix[2, 1] = x[1] * x[1]; M.matrix[2, 2] = x[2] * x[2];

                    M.makeLU();
                    A = M.solutionSystem(m);

                    for (int i = 0; i < 3; i++)
                    {
                        result += A[i] * f.f(x[i]);
                    }
                    l1 = l2;
                    l2 += step;
                }
                return result;
            }

            static double GaussKF(double l1, double l2,double step)
            {
                double result = 0;
                while (l2 <= limes2)
                {

                    for (int i = 0; i < 6; i++)
                    {
                        m[i] = f.integralMoment(l1, l2, i);
                    }

                    M.matrix[0, 0] = m[0];   M.matrix[0, 1] = m[1];   M.matrix[0, 2] = m[2];
                    M.matrix[1, 0] = m[1];   M.matrix[1, 1] = m[2];   M.matrix[1, 2] = m[3];
                    M.matrix[2, 0] = m[2];   M.matrix[2, 1] = m[3];   M.matrix[2, 2] = m[4];

                    double[] y = new double[3];
                    y[0] = -m[3]; y[1] = -m[4]; y[2] = -m[5];

                    M.makeLU();
                    a = M.solutionSystem(y);

                    x[0] = l1; x[2] = l2;
                    x = f.kardano(a, x);
                    Array.Sort(x);

                    M.matrix[0, 0] = 1;             M.matrix[0, 1] = 1;             M.matrix[0, 2] = 1;
                    M.matrix[1, 0] = x[0];          M.matrix[1, 1] = x[1];          M.matrix[1, 2] = x[2];
                    M.matrix[2, 0] = x[0] * x[0];   M.matrix[2, 1] = x[1] * x[1];   M.matrix[2, 2] = x[2] * x[2];

                    M.makeLU();
                    A = M.solutionSystem(m);

                    for (int i = 0; i < 3; i++)
                    {
                        result += A[i] * f.f(x[i]);
                    }
                    l1 = l2;
                    l2 += step;
                }
                return result;
            }

    }
}
