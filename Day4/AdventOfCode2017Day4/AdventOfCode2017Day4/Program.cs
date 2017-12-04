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
            var count = File.ReadAllText("c:\\temp\\input.txt").Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(line => line.Split(' ')).Select(words => words.Length == words.Distinct().Count() ? 1 : 0).Sum();
        }
    }
}
