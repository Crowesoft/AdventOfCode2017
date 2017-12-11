namespace AoC2017Day11
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            Test("ne,ne,ne", 3, 3);
            Test("ne,ne,sw,sw", 0, 2);
            Test("ne,ne,s,s", 2, 2);
            Test("se,sw,se,sw,sw", 3, 3);

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day11\\bin\\Debug", string.Empty);
            Test(File.ReadAllText($"{path}\\input.txt").Replace("\n", string.Empty), 877, 1622);
        }

        public class Hexagon
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }

            public Hexagon n()
            {
                x++;
                z--;

                return this;
            }

            public Hexagon s()
            {
                x--;
                z++;

                return this;
            }

            public Hexagon ne()
            {
                x++;
                y--;

                return this;
            }

            public Hexagon nw()
            {
                z--;
                y++;

                return this;
            }

            public Hexagon se()
            {
                z++;
                y--;

                return this;
            }

            public Hexagon sw()
            {
                x--;
                y++;

                return this;
            }

            public int DistanceFrom(Hexagon a = null)
            {
                if (a == null) a = new Hexagon();

                return (Math.Abs(a.x - this.x) + Math.Abs(a.y - this.y) + Math.Abs(a.z - this.z)) / 2;
            }
        }

        static int[] Parse(string input)
        {
            var position = new Hexagon();
            var max = input.Split(',')
                            .Select(
                                cycle => ((Hexagon)typeof(Hexagon).GetMethod(cycle).Invoke(position, null))
                                    .DistanceFrom())
                                    .Concat(new[] { 0 }).Max();

            return new [] { position.DistanceFrom() , max};
        }
        
        static void Test(string input, int distance, int maxDistance)
        {
            var result = Parse(input);

            if (result[0] == distance && result[1] == maxDistance)
            {
                return;
            }

            var output = $"Input was {input}\n";

            if (result[0] != distance)
                output += $"Distance was {result[0]} but was expected to be {distance}\n\n";

            if (result[1] != maxDistance)
                output += $"Max Distance was {result[1]} but was expected to be {maxDistance}\n\n";
                
            throw new Exception(output);
        }
    }
}
