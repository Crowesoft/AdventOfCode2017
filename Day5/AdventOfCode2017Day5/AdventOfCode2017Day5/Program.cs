using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day5
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("c:\\temp\\input.txt");
            //var input = "0\r\n3\r\n0\r\n1\r\n-3";
            var instructions = input.Split(new string[] { "\n" }, StringSplitOptions.None);
            var pointer = 0;
            var count = 0;
            
            while (pointer >= 0 && pointer < instructions.Length && instructions[pointer] != string.Empty)
            {
                var currentValue = int.Parse(instructions[pointer]);
                var currentPointer = pointer;
                pointer = pointer + currentValue;

                currentValue = currentValue >= 3 ? currentValue - 1 : currentValue + 1;

                instructions[currentPointer] = currentValue.ToString();
                count++;
            }
            
        }
        
    }
}
