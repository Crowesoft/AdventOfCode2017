namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
