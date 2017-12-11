namespace AoC2017Day8
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    
    using Microsoft.JScript.Vsa;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day8\\bin\\Debug", string.Empty);

            Test(File.ReadAllText($"{path}\\test.txt"), 1, 10);
            Test(File.ReadAllText($"{path}\\input.txt"), 5075, 7310);
        }

        static int[] Parse(string input)
        {
            var registers = new Dictionary<string, int>();

            var instructions = input
                                .Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(line => new Instruction(line)).ToList();

            var positive = 0;
            var negative = 0;

            foreach (var instruction in instructions)
            {
                if (!registers.ContainsKey(instruction.Register))
                    registers.Add(instruction.Register, 0);

                if (!registers.ContainsKey(instruction.ConditionRegister))
                    registers.Add(instruction.ConditionRegister, 0);

                var registerValue = registers[instruction.Register];

                var condition = $"{registers[instruction.ConditionRegister]} {instruction.ConditionOperator} {instruction.ConditionValue}";
                
                if (Convert.ToBoolean(Microsoft.JScript.Eval.JScriptEvaluate(condition, VsaEngine.CreateEngine())))
                {
                    if (instruction.Operator == "inc")
                        registerValue += instruction.Value;
                    else
                        registerValue -= instruction.Value;
                }

                if (registerValue > 0)
                    if (registerValue > positive)
                        positive = registerValue;
                else
                    if (registerValue < negative) negative = registerValue;

                registers[instruction.Register] = registerValue;
            }

            return new[] { registers.Values.Max(), Math.Max(Math.Abs(positive), Math.Abs(negative)) };
        }

        static void Test(string input, int largest, int maxLargest)
        {
            var result = Parse(input);

            if (result[0] == largest && result[1] == maxLargest)
                return;

            var output = $"Input was {input}\n";

            if (result[0] != largest)
                output += $"largest was {result[0]} but was expected to be {largest}\n\n";

            if (result[1] != maxLargest)
                output += $"Max largest was {result[1]} but was expected to be {maxLargest}\n\n";

            throw new Exception(output);
        }
    }
    
    public class Instruction
    {
        public Instruction(string input)
        {
            var data = input.Replace("\r", string.Empty).Split(' ');
            this.Register = data[0];
            this.Operator = data[1];
            this.Value = int.Parse(data[2]);
            this.ConditionRegister = data[4];
            this.ConditionOperator = data[5];
            this.ConditionValue = int.Parse(data[6]);
        }
        public string Register { get; set; }
        public string Operator { get; set; }
        public int Value { get; set; }
        public string ConditionRegister { get; set; }
        public string ConditionOperator { get; set; }
        public int ConditionValue { get; set; }
    }
}