using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMethods
{
    public class Matrix
    {
        private int _size;
        public double[,] matrix;

        public Matrix(int size)
        {
            _size = size;
            matrix = new double[_size, _size];
        }

        public void output()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                    System.Console.Out.Write(Math.Round(matrix[i, j], 3) + " ");
                System.Console.Out.Write("\n");
            }
            System.Console.Out.Write("\n" + "\n");
        }

        public double norma()
        {
            double max, cur;
            max = 0;
            for (int i = 0; i < _size; i++)
            {
                cur = 0;
                for (int j = 0; j < _size; j++)
                {
                    cur += Math.Abs(matrix[i, j]);
                }
                if (cur > max)
                    max = cur;
            }
            return max;
        }

        public double[] myltiplyVec(double[] v)
        {
            double[] vector = new double[_size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    vector[i] += v[j] * matrix[i, j];
                }
            }
            return vector;

        }

        public static void outputVec(double[] v, int size)
        {
            for (int i = 0; i < size; i++)
                System.Console.Out.Write(Math.Round(v[i], 5) + " ");
            System.Console.Out.Write("\n");
        }

       static public double[,] myltiply(double[,] M1, double[,] M2, int size)
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
    }

}   


