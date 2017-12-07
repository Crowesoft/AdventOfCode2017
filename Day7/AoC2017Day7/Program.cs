namespace Day7
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new ProgramTree().Import(File.ReadAllText("c:\\temp\\input.txt"));
            var bottomProgram = tree.Root.Name;
            var adjustedWeight = tree.Unbalanced.WeightBalanced;

            var test = new ProgramTree().Import(File.ReadAllText("c:\\temp\\test.txt"));
            var testAdjustedWeight = test.Unbalanced.WeightBalanced;
            var testBottomProgram = test.Root.Name;
        }
    }
}
