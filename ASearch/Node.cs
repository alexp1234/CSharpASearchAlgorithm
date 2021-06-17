using System;
using System.Collections.Generic;
using System.Text;

namespace ASearch
{
    public class Node
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int F { get => this.G + this.H; }
        public int G { get; set; }
        public int H { get; set; }
        public int Type { get; set; }
        public Node Parent { get; set; }
        public Node()
        {
            this.Parent = null;
        }

        public bool IsEqualTo(object input)
        {
            if (input == null)
                return false;
            Node n = (Node)input;
            return Row == n.Row && Col == n.Col;
        }

        public override string ToString()
        {
            return "Node: " + Row + "_" + Col;
        }
    }
}
