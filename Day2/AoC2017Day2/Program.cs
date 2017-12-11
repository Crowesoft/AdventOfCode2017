namespace AoC2017Day2
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day2\\bin\\Debug", string.Empty);

            Test(File.ReadAllText($"{path}\\test.txt"), 9);
            Test(File.ReadAllText($"{path}\\input.txt"), 242);
        }

        static int Parse(string input)
        {
            var results = new List<int>();

            foreach (var numbers in input
                                        .Replace("\r", string.Empty)
                                            .Split('\n').TakeWhile(row => row != string.Empty)
                                                .Select(row => row.Split('\t')
                                                                    .Select(int.Parse)
                                                                        .ToList()))
            {
                for (var i = 0; i < numbers.Count; i++)
                {
                    results.AddRange(from t in numbers.Where((t, j) => j != i) select ((decimal)numbers[i] / (decimal)t) into division where division % 1 == 0 select (int)division);
                }
            }
            
            return results.Sum();
        }

        private static void Test(string input, int sum)
        {
            var result = Parse(input);
            if (result != sum) throw new Exception($"Expected sum to be {sum} but was {result}");
        }
    }
}
