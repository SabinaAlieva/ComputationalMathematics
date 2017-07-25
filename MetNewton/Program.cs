using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathMethods;
using System.Diagnostics;

namespace MetNewton
{
    class Program
    {
        static double[] X, Xk1, current;
        static double[] F;
        static Matrix J, I;
        static int size;
        static double epsilon;

        static void Main(string[] args)
        {
            size = 10;
            epsilon = 0.001;
            X = new double[size];
            Xk1 = new double[size];
            current = new double[size];
            F = new double[size];
            J = new Matrix(size);
            Stopwatch SW = new Stopwatch();

            /*I = new Matrix(3); //        тест работы обратной матрицы
            I.input();
            I.reverse(); 
            MathMethods.Matrix.output(MathMethods.Matrix.myltiply(I.matrix, I.matrixO, 3), 3);
            MathMethods.Matrix.output(I.matrixP, 3);
            MathMethods.Matrix.output(I.matrixQ, 3); */

            double difference;
            int iter = 0;

            //******** 1) Метод Ньютона через поиск обратной матрицы **********************
            
            System.Console.Out.Write("Метод Ньютона через поиск обратной матрицы" + "\n" + "\n");

            SW.Start();
            rewriteX();
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                initializationJ();
                //initJ();
                //initF();
                J.makeLU();
                J.reverse();
                
                current = MathMethods.Matrix.myltiplyVec(J.matrixO, F, size);
               
                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter +  "\n");
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed  + "\n" + "\n");
            

            //******** 2) Метод Ньютона с использованием LU-разложения ****************************************************

            System.Console.Out.Write("\n" + "Метод Ньютона с использованием LU-разложения" + "\n" + "\n");

            SW.Restart();
            rewriteX();
            iter = 0;
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                initializationJ();
                //initJ();
                //initF();
                J.makeLU();
                current = J.solutionSystem(F);
                
                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter + "\n");
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed + "\n" + "\n");

            //******** 3) Модифицированный метод Ньютона через поиск обратной матрицы ****************************

            System.Console.Out.Write("\n" + "Модифицированный метод Ньютона через поиск обратной матрицы" + "\n" + "\n");

            SW.Restart();
            rewriteX();
            //initJ();
            initializationJ();
            J.makeLU();

            MathMethods.Matrix.output(J.matrixL, 10);
            MathMethods.Matrix.output(J.matrixU, 10);


            J.reverse();
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                //initF();
                current = MathMethods.Matrix.myltiplyVec(J.matrixO, F, size);
                //MathMethods.Matrix.outputVec(current, size);

                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter + "\n"); 
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed + "\n" + "\n");
            
            //******** 4) Модифицированный метод Ньютона с использованием LU-разложения *******************************

            System.Console.Out.Write("\n" + "Модифицированный метод Ньютона с использованием LU-разложения" + "\n" + "\n");

            SW.Restart();
            rewriteX();
            //initJ();
            initializationJ();
            J.makeLU();
            iter = 0;
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                //initF();
                current = J.solutionSystem(F);
                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter + "\n");
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed + "\n" + "\n");

        // *********************5) постепенный переход к модернизированному ******************************

            System.Console.Out.Write("\n" + "Поостепенный переход к модифицированному методу " + "\n" + "\n");
       
            int step = 7;
        
            SW.Restart();
            rewriteX();
            iter = 0;
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                initializationJ();
                //initJ();
                //initF();
                J.makeLU();
                if (step > 0)
                {
                    J.reverse();
                    current = MathMethods.Matrix.myltiplyVec(J.matrixO, F, size);
                    // current = J.solutionSystem(F);
                    step--;
                } else
                {
                    current = MathMethods.Matrix.myltiplyVec(J.matrixO, F, size);
                    // current = J.solutionSystem(F);
                    
                }
                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter + "\n");
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed + "\n" + "\n");

            // ******************** 6) Пересчитывание обратной каждые к итераций ****************

            System.Console.Out.Write("\n" + "Пересчитывание каждые к итераций" + "\n" + "\n");

            SW.Restart();
            rewriteX();
            //initJ();
            initializationJ();
            J.makeLU();
            iter = 0;
            step = 5;
            while (true)
            {
                iter++;
                difference = 0;
                initializationF();
                //initF();
                if (iter % 5 == 1)
                {
                    initializationJ();
                    J.makeLU();
                    J.reverse();
                }
                current = J.solutionSystem(F);
                for (int i = 0; i < size; i++)
                {
                    Xk1[i] = X[i] - current[i];
                    if (Math.Abs(Xk1[i] - X[i]) > difference)
                        difference = Math.Abs(Xk1[i] - X[i]);
                    X[i] = Xk1[i];
                }
                if (difference < epsilon)
                    break;

            }
            SW.Stop();
            MathMethods.Vectorr.outputVec(X, size);
            System.Console.Out.Write("Количество итераций: " + iter + "\n");
            System.Console.Out.Write("Затраченное время: " + SW.Elapsed + "\n" + "\n");

            System.Console.In.Read();
        
        }       
        //* * * * * * * * * * * * F U N C T I O N S * * * * * * * * * * * *

        static void initJ()
        {
                J.matrix[0, 0] = 2 * Math.Cos(2 * X[0] - X[1]) - 1.2;
                J.matrix[1, 0] = 1.6 * X[0];
                J.matrix[0, 1] = - Math.Cos(2 * X[0] - X[1]);
                J.matrix[1, 1] = 3 * X[1];
        }

        static void initF()
        {
            F[0] = Math.Sin(2 * X[0] - X[1]) - 1.2 * X[0] - 0.4;
            F[1] = 0.8 * X[0] * X[0] + 1.5 * X[1] * X[1] - 1;
        }

        static void rewriteX()
        {
            X[0] = 0.5; X[1] = 0.5; X[2] = 1.5; X[3] = -1; X[4] = -0.5; 
            X[5] = 1.5; X[6] = 0.5; X[7] = -0.5; X[8] = 1.5; X[9] = -1.5;

            //X[0] = 0.4; X[1] = -0.75;
        }

        static void initializationF() {        
            F[0] = Math.Cos(X[0] * X[1]) - Math.Exp(-3.0 * X[2]) + X[3] * X[4] * X[4] - X[5] - Math.Sinh(2.0 * X[7]) * X[8] + 2.0 * X[9] + 2.0004339741653854440;
            F[1] = Math.Sin(X[0] * X[1]) + X[2] * X[8] * X[6] - Math.Exp(-X[9] + X[5]) + 3.0 * X[4] * X[4] - X[5] * (X[7] + 1.0) + 10.886272036407019994;
            F[2] = X[0] - X[1] + X[2] - X[3] + X[4] - X[5] + X[6] - X[7] + X[8] - X[9] - 3.1361904761904761904;
            F[3] = 2.0 * Math.Cos(-X[8] + X[3]) + X[4] / (X[2] + X[0]) - Math.Sin(X[1] * X[1]) + Math.Cos(X[6] * X[9]) * Math.Cos(X[6] * X[9]) - X[7] - 0.1707472705022304757;
            F[4] = Math.Sin(X[4]) + 2.0 * X[7] * (X[2] + X[0]) - Math.Exp(-X[6] * (-X[9] + X[5])) + 2.0 * Math.Cos(X[1]) - 1.00 / (X[3] - X[8]) - 0.3685896273101277862;
            F[5] = Math.Exp(X[0] - X[3] - X[8]) + X[4] * X[4] / X[7] + Math.Cos(3.0 * X[9] * X[1]) / 2.0 - X[5] * X[2] + 2.0491086016771875115;
            F[6] = X[1] * X[1] * X[1] * X[6] - Math.Sin(X[9] / X[4] + X[7]) + (X[0] - X[5]) * Math.Cos(X[3]) + X[2] - 0.7380430076202798014;
            F[7] = X[4] * (X[0] - 2.0 * X[5]) * (X[0] - 2.0 * X[5]) - 2.0 * Math.Sin(-X[8] + X[2]) + 1.5 * X[3] - Math.Exp(X[1] * X[6] + X[9]) + 3.5668321989693809040;
            F[8] = 7.0 / X[5] + Math.Exp(X[4] + X[3]) - 2.0 * X[1] * X[7] * X[9] * X[6] + 3.0 * X[8] - 3.0 * X[0] - 8.4394734508383257499;
            F[9] = X[9] * X[0] + X[8] * X[1] - X[7] * X[2] + Math.Sin(X[3] + X[4] + X[5]) * X[6] - 0.78238095238095238096;
        }

        static void initializationJ()
        {
            J.matrix[0,0] = -Math.Sin(X[0] * X[1]) * X[1];
            J.matrix[0,1] = -Math.Sin(X[0] * X[1]) * X[0];
            J.matrix[0,2] = 3 * Math.Exp(-(3 * X[2]));
            J.matrix[0,3] = X[4] * X[4];
            J.matrix[0,4] = 2 * X[3] * X[4];
            J.matrix[0,5] = -1;
            J.matrix[0,6] = 0;
            J.matrix[0,7] = -2 * Math.Cosh((2 * X[7])) * X[8];
            J.matrix[0,8] = -Math.Sinh((2 * X[7]));
            J.matrix[0,9] = 2;
            J.matrix[1,0] = Math.Cos(X[0] * X[1]) * X[1];
            J.matrix[1,1] = Math.Cos(X[0] * X[1]) * X[0];
            J.matrix[1,2] = X[8] * X[6];
            J.matrix[1,3] = 0;
            J.matrix[1,4] = 6 * X[4];
            J.matrix[1,5] = -Math.Exp(-X[9] + X[5]) - X[7] - 0.1e1;
            J.matrix[1,6] = X[2] * X[8];
            J.matrix[1,7] = -X[5];
            J.matrix[1,8] = X[2] * X[6];
            J.matrix[1,9] = Math.Exp(-X[9] + X[5]);
            J.matrix[2,0] = 1;
            J.matrix[2,1] = -1;
            J.matrix[2,2] = 1;
            J.matrix[2,3] = -1;
            J.matrix[2,4] = 1;
            J.matrix[2,5] = -1;
            J.matrix[2,6] = 1;
            J.matrix[2,7] = -1;
            J.matrix[2,8] = 1;
            J.matrix[2,9] = -1;
            J.matrix[3,0] = -X[4] * Math.Pow(X[2] + X[0], -2);
            J.matrix[3,1] = -2 * Math.Cos(X[1] * X[1]) * X[1];
            J.matrix[3,2] = -X[4] * Math.Pow(X[2] + X[0], -2);
            J.matrix[3,3] = -2 * Math.Sin(-X[8] + X[3]);
            J.matrix[3,4] = 1 / (X[2] + X[0]);
            J.matrix[3,5] = 0;
            J.matrix[3,6] = -2 * Math.Cos(X[6] * X[9]) * Math.Sin(X[6] * X[9]) * X[9];
            J.matrix[3,7] = -1;
            J.matrix[3,8] = 2 * Math.Sin(-X[8] + X[3]);
            J.matrix[3,9] = -2 * Math.Cos(X[6] * X[9]) * Math.Sin(X[6] * X[9]) * X[6];
            J.matrix[4,0] = 2 * X[7];
            J.matrix[4,1] = -2 * Math.Sin(X[1]);
            J.matrix[4,2] = 2 * X[7];
            J.matrix[4,3] = Math.Pow(-X[8] + X[3], -2);
            J.matrix[4,4] = Math.Cos(X[4]);
            J.matrix[4,5] = X[6] * Math.Exp(-X[6] * (-X[9] + X[5]));
            J.matrix[4,6] = -(X[9] - X[5]) * Math.Exp(-X[6] * (-X[9] + X[5]));
            J.matrix[4,7] = (2 * X[2]) + 2 * X[0];
            J.matrix[4,8] = -Math.Pow(-X[8] + X[3], -2);
            J.matrix[4,9] = -X[6] * Math.Exp(-X[6] * (-X[9] + X[5]));
            J.matrix[5,0] = Math.Exp(X[0] - X[3] - X[8]);
            J.matrix[5,1] = -3.0 / 2.0 * Math.Sin(3 * X[9] * X[1]) * X[9];
            J.matrix[5,2] = -X[5];
            J.matrix[5,3] = -Math.Exp(X[0] - X[3] - X[8]);
            J.matrix[5,4] = 2 * X[4] / X[7];
            J.matrix[5,5] = -X[2];
            J.matrix[5,6] = 0;
            J.matrix[5,7] = -X[4] * X[4] *  Math.Pow(X[7], (-2));
            J.matrix[5,8] = -Math.Exp(X[0] - X[3] - X[8]);
            J.matrix[5,9] = -3.0 / 2.0 * Math.Sin(3 * X[9] * X[1]) * X[1];
            J.matrix[6,0] = Math.Cos(X[3]);
            J.matrix[6,1] = 3 * X[1] * X[1] * X[6];
            J.matrix[6,2] = 1;
            J.matrix[6,3] = -(X[0] - X[5]) * Math.Sin(X[3]);
            J.matrix[6,4] = Math.Cos(X[9] / X[4] + X[7]) * X[9] * Math.Pow(X[4], (-2));
            J.matrix[6,5] = -Math.Cos(X[3]);
            J.matrix[6,6] = Math.Pow(X[1], 3);
            J.matrix[6,7] = -Math.Cos(X[9] / X[4] + X[7]);
            J.matrix[6,8] = 0;
            J.matrix[6,9] = -Math.Cos(X[9] / X[4] + X[7]) / X[4];
            J.matrix[7,0] = 2 *  X[4] * (X[0] - 2 * X[5]);
            J.matrix[7,1] = -X[6] * Math.Exp(X[1] * X[6] + X[9]);
            J.matrix[7,2] = -2 * Math.Cos(-X[8] + X[2]);
            J.matrix[7,3] = 0.15e1;
            J.matrix[7,4] = Math.Pow(X[0] - 2 * X[5], 2);
            J.matrix[7,5] = -4 *  X[4] * (X[0] - 2 * X[5]);
            J.matrix[7,6] = -X[1] * Math.Exp(X[1] * X[6] + X[9]);
            J.matrix[7,7] = 0;
            J.matrix[7,8] = 2 * Math.Cos(-X[8] + X[2]);
            J.matrix[7,9] = -Math.Exp(X[1] * X[6] + X[9]);
            J.matrix[8,0] = -3;
            J.matrix[8,1] = -2 *  X[7] * X[9] * X[6];
            J.matrix[8,2] = 0;
            J.matrix[8,3] = Math.Exp((X[4] + X[3]));
            J.matrix[8,4] = Math.Exp((X[4] + X[3]));
            J.matrix[8,5] = -0.7e1 * Math.Pow(X[5], -2);
            J.matrix[8,6] = -2 * X[1] *  X[7] * X[9];
            J.matrix[8,7] = -2 * X[1] * X[9] * X[6];
            J.matrix[8,8] = 3;
            J.matrix[8,9] = -2 * X[1] *  X[7] * X[6];
            J.matrix[9,0] = X[9];
            J.matrix[9,1] = X[8];
            J.matrix[9,2] = -X[7];
            J.matrix[9,3] = Math.Cos(X[3] + X[4] + X[5]) * X[6];
            J.matrix[9,4] = Math.Cos(X[3] + X[4] + X[5]) * X[6];
            J.matrix[9,5] = Math.Cos(X[3] + X[4] + X[5]) * X[6];
            J.matrix[9,6] = Math.Sin(X[3] + X[4] + X[5]);
            J.matrix[9,7] = -X[2];
            J.matrix[9,8] = X[1];
            J.matrix[9,9] = X[0];
            
        }
    }
}
