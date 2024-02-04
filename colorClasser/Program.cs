using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace colorClasser
{
    class Program
    {
        static StreamReader sr = new StreamReader("colorsList.txt");
        static StreamWriter sw = new StreamWriter("colorCode.txt");
        static StreamWriter sw2 = new StreamWriter("colorFloats.txt");
        static StreamWriter sw3 = new StreamWriter("colorVarsVals.txt");
        static StreamWriter sw4 = new StreamWriter("colorVarsInstance.txt");

        static Char[] c = new Char[6];

        static Char[] name = new Char[32];

        static List<String> colors = new List<String>();
        static void Main(string[] args)
        {
            String s = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                colors.Add(s);

                s = sr.ReadLine();
            }

            colors.Add(s);

            sr.Close();

            colors.Sort();

            String prevName = "";

            Int32 count = 2;

            foreach(String color in colors)
            {
                String[] cs = color.Split('#');

                if (String.Compare(cs[0], prevName) == 0)
                {
                    cs[0] = cs[0] + count.ToString("D1");
                    count++;
                }
                else
                {
                    prevName = cs[0];
                    count = 2;
                }

                var byteArray = Encoding.ASCII.GetBytes(cs[1]);

                int R = Convert.ToInt32(cs[1].Substring(0, 2), 16);
                int G = Convert.ToInt32(cs[1].Substring(2, 2), 16);
                int B = Convert.ToInt32(cs[1].Substring(4, 2), 16);

                sw.WriteLine("#define CColor" + cs[0] + " CColor(" + R.ToString("D1") + ", " + G.ToString("D1") + ", " + B.ToString("D1") + ", 255)");

                sw2.WriteLine("#define " + cs[0] + " float4(" + Convert.ToDouble(R / 255.0f).ToString("F4") + ", " + Convert.ToDouble(G / 255.0f).ToString("F4") + ", " + Convert.ToDouble(B / 255.0f).ToString("F4") + ", 1.0);");

                sw3.WriteLine("void* " + cs[0] + ";");
                sw3.WriteLine("delete " + cs[0] + ";");

                sw4.WriteLine(cs[0] + " = new CColor(" + R.ToString("D1") + ", " + G.ToString("D1") + ", " + B.ToString("D1") + ", 255);");
            }

            sw4.Close();
            sw3.Close();
            sw2.Close();
            sw.Close();
        }
    }
}
