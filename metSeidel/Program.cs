using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metSeidel
{
    class Program
    {
        // суть: A = L + D + R
        static int size;           //размер матрицы системы
        static string[] str;
        static double[] b;         //столбец значений системы
        static double[,] matr; 
        static double[,] matrB;    // матрица obLD * R
        static double[] Xk1, Xk, Xp;    //решение системы x(k+1), x(k), так же начальное приближение в первый раз
        static double differenceVec;  //норма разности векторов

        static void Main(string[] args)
        {
            size = Convert.ToInt32(Console.ReadLine());
            System.Console.Out.Write("\n");

            str = new string[size];
            matr = new double[size, size];
            matrObLD = new double[size, size];
            matrB = new double[size, size];

            for (int j = 0; j < size; j++)
            {
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < size; i++)
                    matr[i, j] = Double.Parse(str[j]);
            }

            for (int i = size - 1; i > -1 ; i--)
            {
                for (int j = i; j > -1 ; j--)
                {
                    matrObLD[i,j] = matr[i, j];
                    matr[i, j] = 0;
                }
            }

            matrObLD = obrMatr(matrObLD);
            //output(matrObLD);
            matrB = myltiply(matrObLD, matr);  //матрица ob(L + D) * R

            Xk1 = new double[size];
            Xk = new double[size];
            Xp = new double[size];
            b = new double[size];

            while (true)
            {
                System.Console.Out.Write("\n" + "вектор значений системы" + "\n");
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < size; i++)
                    b[i] = Double.Parse(str[i]);

                System.Console.Out.Write("\n" + "начальное приближение" + "\n");
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < size; i++)
                    Xk[i] = Double.Parse(str[i]);


                b = myltiplyVec(matrObLD, b); // x(k+1) = B * x(k) + LDobr * b
                

                do
                {
                    differenceVec = 0;
                    for (int i = 0; i < size; i++)
                        Xp[i] = Xk[i];

                    Xk = myltiplyVec(matrB, Xk);

                    for (int i = 0; i < size; i++)
                    {
                        Xk1[i] = -Xk[i] + b[i];
                        if (Math.Abs(Xk1[i] - Xp[i]) > differenceVec)
                            differenceVec = Math.Abs(Xk1[i] - Xp[i]);
                        Xk[i] = Xk1[i];
                    }

                    if (differenceVec < 0.1)
                        break;
                    

                } while (true);

                System.Console.Out.Write("\n" + "решение системы" + "\n");
                outputVec(Xk1);
                outputVec(myltiplyVec(matr, Xk1)); 
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

        static double[,] obrMatr(double[,] M)
        {
            double[,] ObM = new double[size, size];
            double[] vec = new double[size];
            double[] X = new double[size];

            for (int k = 0; k < size; k++)
            {
                vec[k] = 1;

                for (int j = 0; j < size; j++) //  ход по левой матрице
                {
                    X[j] = vec[j] / M[j, j];

                    for (int i = j + 1; i < size; i++)
                        vec[i] -= M[i, j] * X[j];
                }


                for (int i = 0; i < size; i++) { 
                    ObM[i, k] = Math.Round(X[i], 5);
                    vec[i] = 0;
                }
                
            }
            return ObM;
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
