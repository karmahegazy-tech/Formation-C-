using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            char block = 'x';
            int j = n;
            int jg = n-1;
            int jd = n-1;
            int k = 1;

            if (isSmooth == true)
            {
                while (j > 1)
                {
                    j = j - 1;

                                    
                    
                    block = ' ';
                    Console.Write( new string (block, jg));
                    jg = jg - 1;
                                       

                    
                    int h = 1 + (2 * (k-1));
                    k++;
                    block = '+';
                    Console.Write( new string (block, h));

                    Console.WriteLine();

                }
            }


            else if (isSmooth == false)
            {
                while (j > 1)
                {
                    j = j - 1;

                    block = ' ';
                    Console.Write(new string(block, jg));
                    jg = jg - 1;



                    int h = 1 + (2 * (k-1));
                    k++;
                    if (k % 2 == 0)
                    {
                        block = '+';
                        Console.Write(new string(block, h));
                    }
                    else
                    {
                        block = '-';
                        Console.Write(new string(block, h));
                    }
                    
                    Console.WriteLine();
                }
            }

        }
    }
}
