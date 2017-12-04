using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = (from line in File.ReadAllText("c:\\temp\\input.txt").Split(new string[] { "\r\n" }, StringSplitOptions.None) select line.Split(' ') into words select words.Select(word => string.Concat(word.OrderBy(x => x))).ToList() into sorted select sorted.Count == sorted.Distinct().Count() ? 1 : 0).Sum();
        }
    }
}
