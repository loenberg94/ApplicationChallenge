using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\Users\loenb\Desktop\tree.txt";

            Triangle triangle = new Triangle(file);
            Node res = triangle.MaxPath();
            int[] path = res.GetPath();

            Console.WriteLine($"Max sum: {res.GetPathValue()}");
            Console.Write("Path: ");
            for (int i = 0; i < path.Length; i++)
            {
                if (i == path.Length - 1) Console.Write($"{path[i]}");
                else Console.Write($"{path[i]}, ");
            }
            Console.ReadKey();
        }
    }
}
