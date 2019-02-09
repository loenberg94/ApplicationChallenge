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
            Triangle triangle = new Triangle("");

            Node f1 = new Node(4);
            Node f2 = new Node(5);
            Node f3 = new Node(2);
            Node f4 = new Node(3);

            Node t1 = new Node(f1,1,f2);
            Node t2 = new Node(f2,5,f3);
            Node t3 = new Node(f3,9,f4);

            Node s1 = new Node(t1,8,t2);
            Node s2 = new Node(t2,9,t3);

            Node root = new Node(s1,1,s2);

            Node res = triangle.MaxPath(root,10);
            int[] path = res.GetPath();

            Console.WriteLine($"Max sum: {res.GetValue()}");
            Console.Write("Path: ");
            for (int i = 0; i < path.Length; i++)
            {
                if (i == path.Length - 1) Console.Write($"{path[i]}");
                else Console.Write($"{path[i]},");
            }
            Console.ReadKey();
        }
    }
}
