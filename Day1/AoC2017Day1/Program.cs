namespace AoC2017Day1
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day1\\bin\\Debug", string.Empty);

            Test("1212", 6);
            Test("1221", 0);
            Test("123425", 4);
            Test("123123", 12);
            Test("12131415", 4);

            Test(File.ReadAllText($"{path}\\input.txt").Replace("\n", string.Empty), 1268);
        }

        private static int Parse(string input)
        {
            var half = input.Length / 2;
            
            return input.Zip($"{input.Substring(half)}{input.Substring(0, half)}", (x, y) => new Tuple<char, char>(x, y)).Sum(pair => pair.Item1 == pair.Item2 ? int.Parse(pair.Item1.ToString()) : 0);
        }

        private static void Test(string input, int captcha)
        {
            var result = Parse(input);
            if (result != captcha) throw new Exception($"Expected captcha to be {captcha} but was {result}");
        }
    }
}
