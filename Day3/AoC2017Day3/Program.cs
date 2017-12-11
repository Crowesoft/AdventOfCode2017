namespace AoC2017Day3
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Test(1, 0, 0);
            Test(12, 3, 23);
            Test(23, 2, 23);
            Test(1024, 31, 1968);

            Test(289326, 419, 295229);
        }

        static int[] Parse(int input)
        {
            var data = new List<Tuple<Point, int>>();
            var point = new Point(0, 0);
            var dir = new Point(0, -1);
            var largest = 0;

            for (var i = 0; i < input; i++)
            {
                if (input > 1 && largest < input)
                {
                    var sum = Sum(ref data, point.X, point.Y);
                    largest = sum > largest ? sum : largest;
                    data.Add(new Tuple<Point, int>(point, sum));
                }
                
                if ((point.X == point.Y) || ((point.X < 0) && (point.X == -point.Y)) || ((point.X > 0) && (point.X == 1 - point.Y)))
                {
                    var z = dir.X;
                    dir.X = -dir.Y;
                    dir.Y = z;
                }

                point.X += dir.X;
                point.Y += dir.Y;
            }

            return new []{ Distance(0,0, point.X, point.Y) - 1, largest};
        }

        static int Sum(ref List<Tuple<Point, int>> data, int x, int y)
        {
            var result = data.FirstOrDefault(p => p.Item1.X == x - 1 && p.Item1.Y == y - 1)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x && p.Item1.Y == y - 1)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x + 1 && p.Item1.Y == y - 1)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x - 1 && p.Item1.Y == y)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x + 1 && p.Item1.Y == y)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x - 1 && p.Item1.Y == y + 1)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x && p.Item1.Y == y + 1)?.Item2 ?? 0;
            result += data.FirstOrDefault(p => p.Item1.X == x + 1 && p.Item1.Y == y + 1)?.Item2 ?? 0;

            return result == 0 ? 1 : result;
        }

        static int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private static void Test(int input, int distance, int largest)
        {
            var result = Parse(input);
            if (result[0] != distance) throw new Exception($"Expected distance to be {distance} but was {result[0]}");
            if (result[1] != largest) throw new Exception($"Expected largest to be {largest} but was {result[1]}");
        }
    }
}
