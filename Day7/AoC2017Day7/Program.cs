namespace AoC2017Day7
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day7\\bin\\Debug", string.Empty);

            Test(File.ReadAllText($"{path}\\test.txt"), "tknk", 60);
            Test(File.ReadAllText($"{path}\\input.txt"), "eqgvf", 757);
        }

        private static void Test(string input, string bottomProgram, int balancedWeight)
        {
            var tree = new ProgramTree().Import(input);

            if (tree.Root.Name == bottomProgram && tree.Unbalanced.WeightBalanced == balancedWeight) return;

            var output = string.Empty;// $"Input was {input}\n";

            if (tree.Root.Name != bottomProgram) output += $"largest was {tree.Root.Name} but was expected to be {bottomProgram}\n\n";

            if (tree.Unbalanced.WeightBalanced != balancedWeight)
                output +=
                    $"Max largest was {tree.Unbalanced.WeightBalanced} but was expected to be {balancedWeight}\n\n";

            throw new Exception(output);
        }

        public class ProgramNode
        {
            public ProgramNode(string input, ref ProgramTree tree)
            {
                this.tree = tree;
                this.pointers = new List<string>();
                this.LoadInput(input);
            }

            private List<ProgramNode> tree { get; }

            private readonly List<string> pointers;

            public List<ProgramNode> Children
            {
                get
                {
                    return
                        this.pointers.Select(child => this.tree.FirstOrDefault(x => x.Name == child))
                            .Where(node => node != null)
                                .ToList();
                }
            }

            public string Name { get; set; }

            public int Weight { get; set; }

            public List<int> Weights
            {
                get
                {
                    var weights = new List<int>();

                    foreach (var node in this.Children)
                    {
                        var i = 0;
                        this.RecurseForWeights(node, ref i);
                        weights.Add(i);
                    }

                    return weights;
                }
            }

            private void LoadInput(string input)
            {
                var data = input.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                if (data.Length > 1)
                {
                    var children = data[1].Split(',');
                    foreach (var child in children)
                    {
                        this.pointers.Add(child.Trim());
                    }
                }

                data = data[0].Split(' ');

                this.Name = data[0].Trim();
                this.Weight = int.Parse(data[1].Replace("(", "").Replace(")", ""));
            }

            private void RecurseForWeights(ProgramNode node, ref int i)
            {
                foreach (var child in node.Children)
                {
                    this.RecurseForWeights(child, ref i);
                }

                i += node.Weight;
            }

            public bool IsChild(ref ProgramTree tree)
            {
                return tree.FirstOrDefault(x => x.pointers.Contains(this.Name)) != null;
            }

            public int TotalWeight => this.Weights.Sum() + this.Weight;

            public int WeightBalanced
            {
                get
                {
                    var max = this.Weights.Max();
                    var min = this.Weights.Min();
                    var index = this.Weights.IndexOf(max);
                    return this.Children.ElementAt(index).Weight - (max - min);
                }
            }

            public bool AllMatch
            {
                get
                {
                    if (this.Weights.Count == 0) return true;

                    var value = this.Weights[0];
                    for (var i = 1; i < this.Weights.Count; i++)
                    {
                        if (value != this.Weights[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        public class ProgramTree : List<ProgramNode>
        {
            public ProgramNode Root
            {
                get
                {
                    var tree = this;
                    return this.FirstOrDefault(x => !x.IsChild(ref tree));
                }
            }

            public ProgramNode Unbalanced
            {
                get
                {
                    var chain = new ProgramTree();

                    var tree = this.Root;
                    chain.RecurseForUnbalancedChain(this.Root, ref tree);

                    var last = chain.Root;
                    chain.RecurseForLastUnbalanced(chain.Root, ref last);

                    return last;
                }
            }

            private void RecurseForLastUnbalanced(ProgramNode node, ref ProgramNode last)
            {
                foreach (var child in node.Children)
                {
                    if (this.FirstOrDefault(x => x.Name == child.Name) != null)
                    {
                        last = child;
                    }

                    this.RecurseForLastUnbalanced(child, ref last);
                }
            }

            private void RecurseForUnbalancedChain(ProgramNode node, ref ProgramNode root)
            {
                foreach (var child in node.Children)
                {
                    this.RecurseForUnbalancedChain(child, ref root);
                }

                if (node.AllMatch)
                {
                    return;
                }

                this.Add(node);
            }

            public ProgramTree Import(string input)
            {
                var obj = this;
                foreach (var line in input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.Add(new ProgramNode(line, ref obj));
                }

                return this;
            }
        }
    }
}
