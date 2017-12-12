namespace AoC2017Day12
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day12\\bin\\Debug", string.Empty);

            Test(File.ReadAllText($"{path}\\test.txt"), 2);
            Test(File.ReadAllText($"{path}\\input.txt"), 171);
        }
        
        private static void Test(string input, int count)
        {
            var result = Node.Parse(input);
            if (result != count) throw new Exception($"Was expecting count to be {count} but was {result}");
        }
    }

    class Node
    {
        public Node(string input)
        {
            this.Children = new List<int>();

            var data = input.Split(new[] { "<->" }, StringSplitOptions.RemoveEmptyEntries);
            this.Id = int.Parse(data[0].Trim());

            var children = data[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            
            for (var i = 0; i < children.Length; i++)
                this.Children.Add(int.Parse(children[i].Trim()));
        }

        public int Id { get; set; }

        public List<int> Children { get; set; }

        public bool Connected { get; set; }

        public bool Processed { get; set; }

        public static int Parse(string input)
        {
            var nodes = (from line in input.Split('\n')
                         where line != string.Empty
                         select new Node(line))
                         .ToList();

            var count = 0;

            while (nodes.Any(x => !x.Processed))
            {
                Cycle(ref nodes, new List<int>(), nodes.FirstOrDefault(x => !x.Processed));
                count++;
            }

            return count;
        }

        public static void Cycle(ref List<Node> nodes, List<int> nodeChain, Node current)
        {
            nodeChain.Add(current.Id);
            var id = current.Id;
            current.Connected = true;
            var children = nodes.Where(x => x.Children.Contains(id)).ToArray();

            foreach (var child in children)
            {
                if (child.Id == 0)
                {
                    child.Connected = true;
                    continue;
                }

                child.Connected = true;

                if (!nodeChain.Contains(child.Id))
                    Cycle(ref nodes, nodeChain, child);

                child.Processed = true;
            }

            current.Processed = true;
        }
    }
}
