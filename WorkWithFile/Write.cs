using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFile
{
    public class Write
    {
        public void WriteAllLines(string adress, string[] lines)
        {
            System.IO.File.WriteAllLines(adress, lines);
        }

        public void WriteAllLines(string adress, double[] nums)
        {
            string[] lines = new string[nums.Length];
            for (int i = 0; i < nums.Length; i++)
                lines[i] = nums[i].ToString();
            System.IO.File.WriteAllLines(adress, lines);
        }

        public void WriteAllText(string adress, string text)
        {
            System.IO.File.WriteAllText(adress, text);
        }

        public void AddNewText(string adress, string text)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(adress, true))
            {
                file.WriteLine(text);
            }
        }

        public void AddNewLines(string adress, string[] text, int size)
        {
            for (int i = 0; i < size; i++)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(adress, true))
                {
                    file.WriteLine(text[i]);
                }
            }
        }
    }
}
