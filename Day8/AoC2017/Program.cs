namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("c:\\temp\\input.txt");
            var test = File.ReadAllText("c:\\temp\\test.txt");
            
            var registers = new Dictionary<string, int>();
            var instructions = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(line => new Instruction(line)).ToList();
            var positive = 0;
            var negative = 0;

            foreach (var instruction in instructions)
            {
                if(!registers.ContainsKey(instruction.Register)) registers.Add(instruction.Register, 0);
                if (!registers.ContainsKey(instruction.ConditionRegister)) registers.Add(instruction.ConditionRegister, 0);

                var registerValue = registers[instruction.Register];
                var conditionValue = registers[instruction.ConditionRegister];

                var conditionResult = false;

                switch (instruction.ConditionOperator)
                {
                    case "<":
                        conditionResult = conditionValue < instruction.ConditionValue;
                        break;
                    case ">":
                        conditionResult = conditionValue > instruction.ConditionValue;
                        break;
                    case "<=":
                        conditionResult = conditionValue <= instruction.ConditionValue;
                        break;
                    case ">=":
                        conditionResult = conditionValue >= instruction.ConditionValue;
                        break;
                    case "==":
                        conditionResult = conditionValue == instruction.ConditionValue;
                        break;
                    case "!=":
                        conditionResult = conditionValue != instruction.ConditionValue;
                        break;

                    default:
                        throw new Exception();
                }

                if (conditionResult)
                {
                    if (instruction.Operator == "inc")
                    {
                        registerValue += instruction.Value;
                    }
                    else
                    {
                        registerValue -= instruction.Value;
                    }
                }

                if (registerValue > 0)
                {
                    if (registerValue > positive) positive = registerValue;
                }
                else
                {
                    if (registerValue < negative) negative = registerValue;
                }

                registers[instruction.Register] = registerValue;
            }
            
            var largestKey = registers.FirstOrDefault(x => x.Value == registers.Values.Max()).Key;
            var largest = registers.Values.Max();
        }
    }

    public class Instruction
    {
        public Instruction(string input)
        {
            var data = input.Split(new[] { " if " }, StringSplitOptions.RemoveEmptyEntries);
            var tmp = data[0].Split(' ');

            this.Register = tmp[0].Trim();
            this.Operator = tmp[1].Trim();
            this.Value = int.Parse(tmp[2].Trim());

            tmp = data[1].Split(' ');

            this.ConditionRegister = tmp[0].Trim();
            this.ConditionOperator = tmp[1].Trim();
            this.ConditionValue = int.Parse(tmp[2].Trim());

        }

        public string Register { get; set; }
        public string Operator { get; set; }
        public int Value { get; set; }
        public string ConditionRegister { get; set; }
        public string ConditionOperator { get; set; }
        public int ConditionValue { get; set; }
    }
}
