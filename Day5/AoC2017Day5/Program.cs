namespace AdventOfCode2017Day5
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day5\\bin\\Debug", string.Empty);

            Test(File.ReadAllText($"{path}\\test.txt"), 10);
            Test(File.ReadAllText($"{path}\\input.txt"), 24315397);
        }

        static int Parse(string input)
        {
            var instructions = input.Split(new [] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var pointer = 0;
            var count = 0;

            while (pointer >= 0 && pointer < instructions.Length)
            {
                var value = instructions[pointer];
                
                instructions[pointer] = value >= 3 ? value - 1 : value + 1;
                pointer += value;
                count++;
            }

            return count;            
        }

        private static void Test(string input, int steps)
        {
            var result = Parse(input);
            if (result != steps) throw new Exception($"Expected steps to be {steps} but was {result}");
        }
    }
}
