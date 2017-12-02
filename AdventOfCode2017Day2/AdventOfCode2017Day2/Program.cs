using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day2
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var result = Checksum(File.ReadAllText("c:\\temp\\input.txt"));
        }

        static int Checksum(string input)
        {
            var results = new List<int>();

            foreach (var numbers in input.Split('\n').TakeWhile(row => row != string.Empty).Select(row => row.Split('\t').Select(int.Parse).ToList()))
            {
                for (var i = 0; i < numbers.Count; i++)
                {
                    results.AddRange(from t in numbers.Where((t, j) => j != i) select ((decimal)numbers[i] / (decimal)t) into division where division % 1 == 0 select (int)division);
                }
            }
            
            return results.Sum();
        }
    }
}
