using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatif3
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 15;
            if (k == 15)
            {
                Console.Write("C");
                k = 16;
            }
            if (k != 16)
            {
                Console.Write("A");
            }
            else
            {
                Console.Write("O");
                k = 1;
            }
            for (int i = 0; i < k + 6; i++)
            {
                Console.Write("O");
                for (int j = 0; j > k; j++)
                {
                    Console.Write("B");
                }
            }
            if (k != 0 || k == 0)
            {
                Console.Write("N");
            }
            while (k >= 0)
            {
                Console.Write("G");
                k--;
            }
            if (k == (4 - 1 - 4))
            {
                Console.Write("É");
                if (23 != 24 && 25 != 26)
                {
                    Console.WriteLine("!!!!!!");
                }
            }
            Console.ReadLine();
        }
    }
}
