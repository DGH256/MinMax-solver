using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Node_MinMax
    {
        public static int unknownCost = -333444;

        public static int infinit = 9888888;

        public int cost = unknownCost;
        public string name { get; set; }

        public int alpha = -infinit;

        public int beta = infinit;

        public List<Node_MinMax> children = new List<Node_MinMax>();

        Node_MinMax parent = null;

        public Node_MinMax(int nr, string name)
        {
            this.cost = nr;
            this.name = name;
        }

        public Node_MinMax(string name)
        {
            this.name = name;
        }

        public void addChild(Node_MinMax nod)
        {
            nod.parent = this;
            this.children.Add(nod);
        }
    }
}
