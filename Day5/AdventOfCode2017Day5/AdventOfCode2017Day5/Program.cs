namespace AdventOfCode2017Day5
{
    using System;
    using System.Linq;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            //var input = File.ReadAllText("c:\\temp\\input.txt");
            var input = "0\r\n3\r\n0\r\n1\r\n-3";

            var test = Compute(input);
        }

        static int Compute(string input)
        {
            var instructions = input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var pointer = 0;
            var jmps = 0;

            while (pointer >= 0 && pointer < instructions.Length)
            {
                var value = instructions[pointer];
                
                instructions[pointer] = value >= 3 ? value - 1 : value + 1;
                pointer += value;
                jmps++;
            }

            return jmps;
        }        
    }
}
