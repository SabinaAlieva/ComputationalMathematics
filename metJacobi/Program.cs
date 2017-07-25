using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithFile;

namespace metJacobi
{
    class Program
    {
        // суть: A = L + D + R
        static int size;               //размер матрицы системы
        static string[] str;
        static double[] b;             //столбец значений системы
        static double[,] matr;         // исходная 
        static double[,] matrB;        // матрица obD(L + R)
        static double[] X, Xk1, Xk, Xp;        //решение системы x(k+1), x(k), так же начальное приближение в первый раз
        static double differenceVec, iter;  //норма разности векторов

        static void Main(string[] args)
        {
            size = Convert.ToInt32(Console.ReadLine());
            System.Console.Out.Write("\n");

            str = new string[size];
            matr = new double[size, size];
            matrB = new double[size, size];
            Xk1 = new double[size];
            Xk = new double[size];
            Xp = new double[size];
            X = new double[size];
            b = new double[size];

            Read f = new Read();
            string adress = "D:\\Visual studio\\Projects\\metJacobi\\matrix.txt";
            matr = f.ReadMatrix(adress);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++){
                    matrB[i, j] = matr[i, j]; 
                }
            }
            

            //output(matr);
            //output(matrB);

            /* for (int j = 0; j < size; j++)
            {
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < size; i++) {
                    matr[j, i] = Double.Parse(str[i]);
                    matrB[j, i] = Double.Parse(str[i]);
                }
            } */

            for (int i = 0; i < size; i++)
            {
                matrB[i,i] = 0;    // теперь это матрица  L + R
                for (int j = 0; j < size; j++)
                    matrB[i, j] /= matr[i, i];
            }

            while (true)
            {
                System.Console.Out.Write("\n" + "вектор значений системы" + "\n");
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < size; i++)
                    b[i] = Double.Parse(str[i]);

                System.Console.Out.Write("\n" + "начальное приближение" + "\n");
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < size; i++)
                {
                    Xk[i] = Double.Parse(str[i]);
                    X[i] = Double.Parse(str[i]);
                }

                for (int i = 0; i < size; i++)
                    b[i] /= matr[i, i];


                do
                {
                    iter++;
                    for (int i = 0; i < size; i++)
                        Xp[i] = Xk[i];

                    Xk = myltiplyVec(matrB, Xk);
                    differenceVec = 0;

                    for (int i = 0; i < size; i++)
                    {
                        Xk[i] = -Xk[i] + b[i];
                        if (Math.Abs(Xk[i] - Xp[i]) > differenceVec)
                            differenceVec = Math.Abs(Xk[i] - Xp[i]);

                    }

                    if (differenceVec < 1e-6)
                        break;

                } while (true);

                System.Console.Out.Write("\n" + "решение системы методом якоби" + "\n");
                outputVec(Xk);
                System.Console.Out.Write("\n" + "значение при решении методом якоби" + "\n");
                outputVec(myltiplyVec(matr, Xk));
                System.Console.Out.Write("\n" + "количество итераций " + iter + "\n");

                iter = 0;
                for (int i = 0; i < size; i++)
                    Xk[i] = X[i];

                do
                 {
                     iter++;
                 
                     for (int i = 0; i < size; i++)
                         Xp[i] = Xk[i];
                    
                     differenceVec = 0;

                     for (int i = 0; i < size; i++)
                     {
                         Xk1[i] = b[i];
                         for (int j = 0; j < size; j++)
                         {
                             Xk1[i] -= Xk[j] * matrB[i, j];
                         }
                         //Xk1[i] += b[i];
                         Xk[i] = Xk1[i];
                         

                         if (Math.Abs(Xk1[i] - Xp[i]) > differenceVec)
                                 differenceVec = Math.Abs(Xk1[i] - Xp[i]);
                         
                     }

                     if (differenceVec < 1e-6)
                         break;
                 } while (true);

                System.Console.Out.Write("\n" + "решение системы методом зейделя" + "\n");
                outputVec(Xk);
                System.Console.Out.Write("\n" + "значение при решении методом зейделя" + "\n");
                outputVec(myltiplyVec(matr, Xk));
                System.Console.Out.Write("\n" + "количество итераций " + iter + "\n");
             
            }
               System.Console.In.Read();
        }

        //********** F U N C T I O N S *******

        static double[,] myltiply(double[,] M1, double[,] M2)
        {
            double[,] M = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        M[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            }
            return M;
        }

        static double[] myltiplyVec(double[,] M, double[] v)
        {
            double[] vector = new double[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    vector[i] += v[j] * M[i, j];
                }
            }
            return vector;
        }

        static double normaMatr(double[,] M)
        {
            double max, cur;
            max = 0;
            for (int i = 0; i < size; i++)
            {
                cur = 0;
                for (int j = 0; j < size; j++)
                {
                    cur += Math.Abs(M[i, j]);
                }
                if (cur > max)
                    max = cur;
            }
            return max;
        }

        static void output(double[,] M)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    System.Console.Out.Write(Math.Round(M[i, j], 3) + " ");
                System.Console.Out.Write("\n");
            }
            System.Console.Out.Write("\n" + "\n");
        }

        static void outputVec(double[] v)
        {
            for (int i = 0; i < size; i++)
                System.Console.Out.Write(Math.Round(v[i], 5) + " ");
            System.Console.Out.Write("\n");
        }
    }
}
