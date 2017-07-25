using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMethods
{
    public class Matrix
    {
        public int size;
        public bool isLU = false;
        public double[,] matrix;
        public double[,] matrixP, matrixQ, matrixL, matrixU, matrixO;
        private double determinant;
        private int rank;

        public Matrix(int _size)
        {
            size = _size;
            matrix = new double[size, size];
        }

        public void input()
        {
            string[] str;
            for (int j = 0; j < size; j++)
            {
                str = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < size; i++)
                {
                    matrix[j, i] = Double.Parse(str[i]);
                }
            }

        }

       
        public static void output(double[,] M, int s)
        {
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                    System.Console.Out.Write(Math.Round(M[i, j], 5) + " ");
                System.Console.Out.Write("\n");
            }
            System.Console.Out.Write("\n" + "\n");
        }

        public double getDeterminant()
        {
            if (isLU)
                return determinant;
            else
            {
                makeLU();
                return determinant;
            }
        }

        public double getRank()
        {
            if (isLU)
                return rank;
            else
            {
                makeLU();
                return rank;
            }
        }

        public double norma()
        {
            double max, cur;
            max = 0;
            for (int i = 0; i < size; i++)
            {
                cur = 0;
                for (int j = 0; j < size; j++)
                {
                    cur += Math.Abs(matrix[i, j]);
                }
                if (cur > max)
                    max = cur;
            }
            return max;
        }

        public void makeLU(){
            isLU = true;
            bool matrIsNorm = true;

            matrixL = new double[size, size];
            matrixU = new double[size, size];
            matrixP = new double[size, size];
            matrixQ = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrixU[i, j] = matrix[i, j];
                }
                matrixP[i, i] = 1;
                matrixQ[i, i] = 1;
            }

            int lineNum1, lineNum2;
            int numSwap = 0;
            double max;
            for (int i = 0; i < size; i++) //цикл построения верхнеи нижнетреугольных матриц
            {
                if ((matrixU[i, i] == 0))
                {
                    matrIsNorm = false;
                    for (int j = i + 1; j < size; j++)
                    {
                        if (matrixU[j, i] != 0)
                        {
                            lineNum1 = i;
                            lineNum2 = j;
                            SwapLine(matrixU, lineNum1, lineNum2, size);
                            SwapLine(matrixL, lineNum1, lineNum2, size);
                            SwapLine(matrixP, lineNum1, lineNum2, size);
                            i = i - 1;
                            numSwap++;
                            matrIsNorm = true;
                            break;
                        }
                    }
                    if (i >= rank)
                        break;
                    if (matrIsNorm == false)
                    {
                        SwapColumn(matrixU, i, rank - 1, size);
                        SwapColumn(matrixQ, i, rank - 1, size);
                        rank--;
                        i = i - 1;
                        numSwap++;
                    }
                }
                else
                {
                    matrIsNorm = true;
                    max = matrixU[i, i];
                    lineNum1 = i;
                    lineNum2 = i;
                    for (int j = i; j < size; j++) // еще раз по нижним строкам
                    {
                        if ((Math.Abs(matrixU[j, i]) > max) && (matrixU[j, i] != 0))
                        {
                            max = matrixU[j, i];
                            lineNum2 = j;
                        }
                    }

                    if (lineNum1 != lineNum2)
                    {
                        SwapLine(matrixU, lineNum1, lineNum2, size);
                        SwapLine(matrixL, lineNum1, lineNum2, size);
                        SwapLine(matrixP, lineNum1, lineNum2, size);
                        numSwap++;
                    }

                    double coeff;
                    for (int k = i + 1; k < size; k++) // по строкам под текущей
                    {
                        if (matrixU[i, i] != 0)
                        {
                            coeff = matrixU[k, i] / matrixU[i, i];
                            for (int j = i; j < size; j++) // по столбцам
                            {
                                matrixU[k, j] = matrixU[k, j] - matrixU[i, j] * coeff;
                            }
                            matrixL[k, i] = coeff;
                        }
                    }
                }
            }

            for (int i = 0; i < size; i++)
                matrixL[i, i] = 1;

            for (int i = 0; i < size; i++)
                determinant *= matrixU[i, i];

            if (numSwap % 2 != 0)
                determinant = determinant * (-1);
        }

        public double[] solutionSystem(double[] vec)
        {
                if (!isLU)
                    makeLU();

                double[] X = new double[size];
                double[] Y = new double[size];
                vec = myltiplyVec(matrixP, vec, size); //преобразовываем к виду после перестановок строк
                bool matrIsNorm = true;
                
                for (int j = rank; j < size; j++) //проверка системы на совместность
                {
                    if (vec[j] != 0)
                    {
                        matrIsNorm = false;
                        break;
                    }
                }

                for (int j = 0; j < size; j++) // обратный ход по левой матрице
                {
                    Y[j] = vec[j] / matrixL[j, j];

                    for (int i = j; i < size; i++)
                        vec[i] -= matrixL[i, j] * Y[j];
                }
                
                for (int j = size - 1; j > -1; j--) //обратный ход по правой
                {
                    X[j] = Y[j] / matrixU[j, j];
                    
                    for (int i = 0; i < j; i++)
                        Y[i] -= matrixU[i, j] * X[j];
                }
                
                X = myltiplyVec(matrixQ, X, size);   //убираем влияние перестановки столбцов

                //if (matrIsNorm)
                return X;
                //else
            
        }

        static void SwapLine(double[,] M, int l1, int l2, int size)
        {
            double d;
            for (int i = 0; i < size; i++)
            {
                d = M[l1, i];
                M[l1, i] = M[l2, i];
                M[l2, i] = d;
            }
        }

        static void SwapColumn(double[,] M, int c1, int c2, int size)
        {
            double d;
            for (int i = 0; i < size; i++)
            {
                d = M[i, c1];
                M[i, c1] = M[i, c2];
                M[i, c2] = d;
            }
        }

        public void reverse()
        {
            if (!isLU)
                makeLU();

            double[] vec = new double[size];
            matrixO = new double[size, size];

            for (int k = 0; k < size; k++)
            {
                vec[k] = 1;
                vec = myltiplyVec(matrixP, vec, size);

                for (int j = 0; j < size; j++)
                {
                    for (int i = j + 1; i < size; i++)
                        vec[i] -= matrixL[i, j] * vec[j];
                }

                for (int j = size - 1; j > -1; j--)
                {
                    vec[j] /= matrixU[j, j];

                    for (int i = 0; i < j; i++)
                        vec[i] -= matrixU[i, j] * vec[j];
                }

                for (int i = 0; i < size; i++)
                    matrixO[i, k] = vec[i];
                    //matrixO[i, k] = Math.Round(vec[i], 5);

                for (int i = 0; i < size; i++)
                    vec[i] = 0;
            }
            
        }

        public static double[] myltiplyVec(double[,] M, double[] v, int size)
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


        public static double[,] myltiply(double[,] M1, double[,] M2, int size)
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


