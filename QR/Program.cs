using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR
{
    class Program
    {
        static int size;
        static string[] str;
        static double[] b;
        static double[,] matr, T, Tall;
        static double[] vec;
        static double[] X;
        static double sin, cos;


        static void Main(string[] args)
        {
            size = Convert.ToInt32(Console.ReadLine());
            System.Console.Out.Write("\n");

            matr = new double[size, size];
            T = new double[size, size];
            Tall = new double[size, size];
            b = new double[size];
            X = new double[size];

            for (int i = 0; i < size; i++)
            {
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < size; j++)
                {
                    matr[i,j] = Double.Parse(str[j]);
                }
                T[i, i] = 1;
                Tall[i, i] = 1;
            }

            System.Console.Out.Write("\n");

            for (int j = 0; j < size - 1; j++) // столбцы
            {
                for (int i = j + 1; i < size; i++) //строки (элементы в этом столбце под диагональю)
                {
                    if (matr[i, j] != 0){
                        cos = Math.Round(matr[j,j] / (Math.Sqrt( Math.Pow (matr[j,j], 2) + Math.Pow (matr[i,j], 2))),8);
                        sin = Math.Round(matr[i,j] / (Math.Sqrt( Math.Pow (matr[j,j], 2) + Math.Pow (matr[i,j], 2))),8);
                        //sin = Math.Sqrt(1 - Math.Pow(cos, 2));

                        T[j,j] = cos;   T[j,i] = sin;
                        T[i,j] = -sin;  T[i,i] = cos;

                        matr = myltiply(T, matr);
                        Tall = myltiply(T, Tall);

                        T[i,i] = 1; T[j,i] = 0;
                        T[i,j] = 0; T[j,j] = 1;
                    }
                }
            }
                output(matr);
                output(Tall);
                output(myltiplyT(Tall,Tall));
            
                for ( ; ; )
                {
                    str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < size; j++) 
                        b[j] = Double.Parse(str[j]);
                    System.Console.Out.Write("\n");

                    b = myltiplyVec(Tall, b);

                    for (int j = size - 1; j > -1; j--) //обратный ход по правой
                    {
                        X[j] = b[j] / matr[j, j];

                        for (int i = 0; i < j; i++)
                            b[i] -= matr[i, j] * X[j];
                    }


                    outputVec(X);
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

        static double[,] myltiplyT(double[,] M1, double[,] M2)
        {
            double[,] M = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        M[i, j] += M1[k, i] * M2[k, j];
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

        static void output(double[,] M)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    System.Console.Out.Write(Math.Round(M[i, j],3) + " ");
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
