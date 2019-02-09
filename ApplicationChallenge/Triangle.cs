using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationChallenge
{
    class Triangle
    {
        private Node root;
        private int numberOfNodes;

        public Triangle(string triangle)
        {

        }

        private bool IsValidPath(Node current,Node prev)
        {
            return prev == null || prev.IsEven() != current.IsEven();
        }
        
        private Node MaxPath(Node[] vNodes,Node prev, Node current, int acc, int depth)
        {
            current.Depth = depth;
            
            if (IsValidPath(current, prev))
            {
                if (current.Parent == null) // Unvisited node
                {
                    current.Parent = prev;
                    current.BestValue = acc + current.Value;

                    if (current.IsLeaf())
                    {
                        vNodes[current.Id] = current;
                        return current;
                    }

                    Node lPath = MaxPath(vNodes, current, current.Left, acc + current.Value, depth + 1);
                    Node rPath = MaxPath(vNodes, current, current.Right, acc + current.Value, depth + 1);
                    
                    bool goLeft = lPath.BestValue > rPath.BestValue;
                    vNodes[current.Id] = goLeft ? lPath:rPath;
                    return goLeft ? lPath : rPath;
                }
                else // Already visited node
                {
                    if (current.Parent.BestValue < prev.BestValue)
                    {
                        int diff = prev.BestValue - current.Parent.BestValue;
                        current.Parent = prev;
                        current.BestValue = current.BestValue + diff;
                    }
                    return vNodes[current.Id];
                }
            }
            return new Node(int.MinValue);
        }
        
        public Node MaxPath(Node tree = null, int nodes = -1)
        {
            //return MaxPathDP(new Path[numberOfNodes], root, 0, 0);
            return MaxPath(new Node[nodes==-1?numberOfNodes:nodes],null,tree??root,0, 0);
        }
    }
}
