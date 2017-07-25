using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LUmatr
{
    class Program
    {
        static double[,] matrMain;
        static double[,] matr;
        static double[,] matrL;
        static double[,] matrU;
        static double[,] matrStr;
        static double[,] matrCol;

        static int sizeStr;
        static int sizeCol;
        static double max;
        static int lineNum1, lineNum2;
        static int colmnNum1, colmnNum2;

        static void output(double[,] M, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    System.Console.Out.Write(M[i, j] + " ");
                System.Console.Out.Write("\n");
            }
            System.Console.Out.Write("\n");
        }

        static void SwapStr(double[,] M, int l1, int l2, int sizeC)
        {
            double current;
            for (int i = 0; i < sizeC; i++)
            {
                current = M[l1, i];
                M[l1, i] = M[l2, i];
                M[l2, i] = current;
            }
        }

        static void SwapColumn(double[,] M, int c1, int c2, int sizeS)
        {
            double current;
            for (int i = 0; i < sizeS; i++)
            {
                current = M[i, c1];
                M[i, c1] = M[i, c2];
                M[i, c2] = current;
            }
        }


        static double[,] multiply(double[,]M1, double[,]M2, int s, int c, int m)
        {
            double a = 0;
            double[,] M;
                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            a += matrStr[i, k] * matrMain[k, j];
                        }
                        M[i, j] = a;
                        a = 0;
                    }
                }
                return M;
        }

        static void Main(string[] args)
        {

            sizeStr = Convert.ToInt32(System.Console.ReadLine());
            sizeCol = Convert.ToInt32(System.Console.ReadLine());
            matr = new double[sizeStr, sizeCol];
            matrMain = new double[sizeStr, sizeCol];
            matrStr = new double[sizeStr, sizeStr];
            matrCol = new double[sizeCol, sizeCol];


            for (int i = 0; i < sizeStr; i++)
            {   
                //List<int> str = Console.ReadLine().Split().Select(Int32.Parse).ToList();
                for (int j = 0; j < sizeCol; j++)
                {
                    //matr[i, j] = str[j];
                    matrMain[i, j] = Convert.ToInt32(System.Console.ReadLine());
                    matr[i, j] = matrMain[i, j];
                }
                matrStr[i, i] = 1;
                matrL[i, i] = 1;
            }

            for (int i = 0; i < sizeCol; i++){
                matrCol[i,i] = 1;
            }


            for (int i = 0; i < sizeStr; i++)
            {
                if (i < sizeCol){
                
                    max = matrMain[i, i];
                    lineNum1 = i;
                    for (int j = i; j < sizeStr; j++) // еще раз по нижним строкам
                    {
                        if ((Math.Abs(matrMain[j, i]) > max) && (matrMain[j, i] != 0))
                        {
                            max = matrMain[j, i];
                            lineNum2 = j;
                        }
                    }

                    SwapStr(matrMain, lineNum1, lineNum2, sizeStr);
                    SwapStr(matrStr, lineNum1, lineNum2, sizeStr);

                    double coeff;
                    for (int k = i + 1; k < sizeStr; k++) // по строкам под текущей
                    {
                        if (matrMain[i, i] != 0)
                        {
                            coeff = matrMain[k, i] / matrMain[i, i];
                            for (int j = i; j < sizeCol; j++) // по столбцам
                            {
                                matrMain[k, j] = matrMain[k, j] - matrMain[i, j] * coeff;
                            }
                            matrStr[k, i] = coeff;
                        }
                    }
                }



                output(matr, sizeStr, sizeCol);
                matr = myltiply(matrStr,matrMain, sizeStr, sizeCol, sizeStr);
                output(matr, sizeStr, sizeCol);
                matr = myltiply(matrL, matrU, sizeStr, sizeCol, sizeStr);
                output(matr, sizeStr, sizeCol);
                

                System.Console.In.Read();
            }
        }
    }
}
