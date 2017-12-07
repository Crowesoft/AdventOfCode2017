namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                return this.pointers.Select(child => this.tree.FirstOrDefault(x => x.Name == child)).Where(node => node != null).ToList();
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
}
