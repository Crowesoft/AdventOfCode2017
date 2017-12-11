namespace AoC2017Day4
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day4\\bin\\Debug", string.Empty);

            Test("abcde fghij", 1);
            Test("abcde xyz ecdab", 0);
            Test("a ab abc abd abf abj", 1);
            Test("iiii oiii ooii oooi oooo", 1);
            Test("oiii ioii iioi iiio", 0);

            Test(File.ReadAllText($"{path}\\input.txt"), 186);
        }

        static int Parse(string input)
        {
            var lines = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            return (from line in lines
                        select line.Split(' ') into words
                            select words.Select(word => string.Concat(word.OrderBy(x => x))).ToList() 
                                into sorted
                                    select sorted.Count == sorted.Distinct().Count() ? 1 : 0).Sum();
        }

        private static void Test(string input, int valid)
        {
            var result = Parse(input);
            if (result != valid) throw new Exception($"Expected valid count to be {valid} but was {result}");
        }
    }
}
