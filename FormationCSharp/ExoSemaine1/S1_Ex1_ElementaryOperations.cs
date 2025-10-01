using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoSemaine1
{
    public static class ElementaryOperations
    {


        public static void BasicOperation(int a, int b, char operation)
        {
            int c;

            if (operation == '+')
            {
                c = a + b;
                Console.WriteLine($"{a} {operation} {b} = {c}");
            }
            else if (operation == '-')
            {
                c = a - b;
                Console.WriteLine($"{a} {operation} {b} = {c}");
            }
            else if (operation == '*')
            {
                c = a * b;
                Console.WriteLine($"{a} {operation} {b} = {c}");
            }
            else if (operation == '^')
            {
                Pow(a, b);
            }
            else if (operation == '/')
            {
                if (b == 0)
                {
                    Console.WriteLine($"{a} {operation} {b} = operation invalide");
                }
                else
                {
                    IntegerDivision(a, b);
                }
            }
            else
            {
                Console.WriteLine($"{a} {operation} {b} = operation invalide");
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            int c;
            int q;
            c = a / b;
            q = a % b;
            Console.WriteLine($"{a} = {c} * {b} + {q}");
        }

        public static void Pow(int a, int b)
        {
            

            if (b == 0)
            {
                Console.WriteLine($"{a} ^ {b} = operation invalide");
            }
           else
            {
                double c = Math.Pow(a, b);
                Console.WriteLine($"{a} ^ {b} = {c}");
            }
        }
    }
}
