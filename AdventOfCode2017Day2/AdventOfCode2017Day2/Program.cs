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
            return
                input.Split('\n')
                    .TakeWhile(row => row != string.Empty)
                    .Select(row => row.Split('\t').Select(int.Parse).ToList())
                    .Select(numbers => numbers.Max() - numbers.Min())
                    .ToList()
                    .Sum();
        }
    }
}
