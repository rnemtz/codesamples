using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.Internal
{
    public class Graph
    {
        public GraphNode Root { get; set; }

        public Graph()
        {
           Root = new GraphNode(50);
           var n1 = new GraphNode(25);
           Root.Nodes.Add(n1);
           Root.Nodes.Add(new GraphNode(15));
           Root.Nodes.Add(new GraphNode(20));
           Root.Nodes.Add(new GraphNode(10));

           var n2 = new GraphNode(53);

            n1.Nodes.Add(n2);
            Root.Nodes.Add(new GraphNode(1));
            Root.Nodes.Add(new GraphNode(2));
            Root.Nodes.Add(new GraphNode(3));

        }

        public void PrintDfs()
        {
            Dfs(Root);
        }

        private void Dfs(GraphNode node)
        {
            if (node == null) return;
            foreach (var nd in node.Nodes)
            {
                if (nd.Visited) continue;

                Dfs(nd);
                Console.WriteLine(nd.Value);
                nd.Visited = true;
            }
        }

        public void PrintBdf()
        {
            if (Root == null) return;
            var q = new Queue<GraphNode>();
            var current = Root;
            q.Enqueue(current);

            while (q.Any())
            {
                current = q.Dequeue();
                current.Visited = true;
                Console.WriteLine(current.Value);

                foreach (var c in current.Nodes)
                {
                    if (!c.Visited)
                    {
                        c.Visited = true;
                        q.Enqueue(c);
                    }
                }
            }
        }
    }

    public class GraphNode
    {
        public int Value { get; set; }
        public bool Visited { get; set; }
        public List<GraphNode> Nodes { get; set; }

        public GraphNode(int value)
        {
            Value = value;

            Nodes = new List<GraphNode>();
        }
    }
}
