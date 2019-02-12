using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationChallenge
{
    class Triangle
    {
        private readonly Node root;
        private readonly int numberOfNodes;
        
        private int TreeSize(int depth)
        {
            int size = 0;
            for (int i = 0; i < depth + 1; i++) size += i;
            return size;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private Node StringToTree(string[] tree)
        {
            List<List<Node>> nodeStructure = new List<List<Node>>(tree.Length); // initialize node structure
            for (int i = 0; i < tree.Length; i++) nodeStructure.Add(new List<Node>());

            for (int i = tree.Length - 1; i >= 0; i--) // Creating tree from the bottom up
            {
                MatchCollection digits = Regex.Matches(tree[i], "\\d+"); // Gets all number occurrences in line
                for (int j = 0; j < digits.Count; j++)
                {
                    int value = int.Parse(digits[j].Value);
                    if (i != tree.Length - 1)
                    {
                        Node left = nodeStructure[i + 1][j];
                        Node right = nodeStructure[i + 1][j + 1];
                        nodeStructure[i].Add(new Node(left,value,right));
                    }
                    else
                    {
                        nodeStructure[i].Add(new Node(value));
                    }
                }
            }
            return nodeStructure[0][0];
        }

        private bool IsValidPath(Node current,Node prev)
        {
            return prev == null || prev.IsEven() != current.IsEven();
        }
        
        /// <summary>
        /// Recursivly traverses the tree to the bottom, utilizes memoization in Nodes as BestValue and vNodes,
        /// to keep track of current best path to a given Node. 
        /// This ensures that a path only will be followed once.
        /// </summary>
        /// <param name="vNodes">Visited nodes, index is null if not visited otherwise leaf node of current path</param>
        /// <param name="parent"></param>
        /// <param name="current"></param>
        /// <param name="acc">Current path value</param>
        /// <param name="depth">Depth in tree</param>
        /// <returns>Bottom node of the maximum path</returns>
        private Node MaxPath(Node[] vNodes,Node parent, Node current, int acc, int depth)
        {
            current.Depth = depth;
            
            if (IsValidPath(current, parent))
            {
                if (current.Parent == null) // Unvisited node
                {
                    current.Parent = parent;
                    current.BestValue = acc + current.Value;

                    if (current.IsLeaf())
                    {
                        vNodes[current.Id] = current;
                        return current;
                    }

                    Node lPath = MaxPath(vNodes, current, current.Left, acc + current.Value, depth + 1);  // Follow left branch
                    Node rPath = MaxPath(vNodes, current, current.Right, acc + current.Value, depth + 1); // Follow right branch
                    
                    bool goLeft = lPath.BestValue > rPath.BestValue;
                    vNodes[current.Id] = goLeft ? lPath:rPath;
                    return goLeft ? lPath : rPath;
                }
                else // Already visited node
                {
                    current.Parent.UpdateBestValue();
                    if (current.Parent.BestValue < parent.BestValue) // Change parent of current node
                    {
                        int diff = parent.BestValue - current.Parent.BestValue;
                        current.Parent = parent;
                        current.BestValue = current.BestValue + diff;
                    }
                    else
                    {
                        current.BestValue = current.Parent.BestValue + current.Value;
                    }
                    current.FullyVisited = true;
                    return vNodes[current.Id];
                }
            }
            return new Node(int.MinValue);
        }
        
        public Node MaxPath(Node tree = null, int nodes = -1)
        {
            return MaxPath(new Node[nodes==-1?numberOfNodes:nodes],null,tree??root,0, 0);
        }

        public Triangle(Node tree, int size)
        {
            root = tree;
            numberOfNodes = size;
        }

        public Triangle(string fileName)
        {
            // Error handling, such as exceptions, should be handled where the object is created and used, not here.
            string[] lines = File.ReadAllLines(fileName);
            root = StringToTree(lines);
            numberOfNodes = TreeSize(lines.Length);
        }
    }
}
