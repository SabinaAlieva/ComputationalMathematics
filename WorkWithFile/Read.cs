using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WorkWithFile
{
    public class Read
    {
        public double[] ReadFileByLine(string adress, int size)
        {
            int counter = 0;
            string line;
            double[] lines = new double[size];
 
            System.IO.StreamReader file =
                new System.IO.StreamReader(adress);
            while ((line = file.ReadLine()) != null)
            {
                lines[counter] = Double.Parse(line);
                counter++;
            }

            file.Close();
            return lines;
        }

        public double[,] ReadMatrix(string adress)
        {
            string[] lines = File.ReadAllLines(adress);
            double[,] num = new double[lines.Length, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                    num[i, j] = Int32.Parse(temp[j]);
            }
            return num;
        }

        public double[] ReadBitmapPicture(string adress, int size)
        {
            Image im = Image.FromFile(adress); 
            Bitmap bmp = new Bitmap(im);

            double[] vecEntryLayer = new double[size];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Color c = bmp.GetPixel(i, j);
                    //if (c.GetBrightness() > 0.5)
                    if (c.ToArgb() == Color.White.ToArgb())
                        vecEntryLayer[i * 100 + j] = 0;
                    else vecEntryLayer[i * 100 + j] = 1;
                }
            }
            return vecEntryLayer;
        }
    }
}
