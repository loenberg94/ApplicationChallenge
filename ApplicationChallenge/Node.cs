using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationChallenge
{
    class Node
    {
        private static int ID = 0;
        public int Id { get; } // Used to memoize

        internal Node Left { get; }
        internal Node Right { get; }
        internal Node Parent { get; set; }
        public int BestValue { get; set; } = int.MinValue;
        public readonly int Value;
        public int Depth { get; set; } = 0;

        public Node(Node left, int value, Node right) // Node constructor
        {
            Left = left;
            Right = right;
            Value = value;
            Id = ID;
            ID++;
        }

        public Node(int value) // Leaf constructor
        {
            Value = value;
            Id = ID;
            ID++;
        }

        public bool IsEven()
        {
            return Value % 2 == 0;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }

        public int[] GetPath()
        {
            int[] path = new int[Depth + 1];

            Node tmp = this;
            while (tmp != null)
            {
                path[tmp.Depth] = tmp.Value;
                tmp = tmp.Parent;
            }

            return path;
        }

        public int GetValue()
        {
            int sum = 0;

            Node tmp = this;
            while (tmp != null)
            {
                sum += tmp.Value;
                tmp = tmp.Parent;
            }

            return sum;
        }
    }
}
