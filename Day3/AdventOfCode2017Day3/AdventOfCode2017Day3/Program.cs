using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day3
{
    using System.CodeDom;
    using System.Drawing;
    using System.Net;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;

    class Program
    {
        static void Main(string[] args)
        {
            var test = ValueLargerThanInput(289326);
        }
        
        static int ValueLargerThanInput(int input)
        {
            var data = new List<Tuple<Point, int>>();
            var point = new Point(0, 0);
            var dir = new Point(0, -1);
            
            for (var i = 0; i < input; i++)
            {
                var sum = SumOfAdjacent(ref data, point.X, point.Y);

                if (sum > input) return sum;

                data.Add(new Tuple<Point, int>(point, sum));
                
                if ((point.X == point.Y) || ((point.X < 0) && (point.X == -point.Y)) || ((point.X > 0) && (point.X == 1 - point.Y)))
                {
                    var z = dir.X;
                    dir.X = -dir.Y;
                    dir.Y = z;
                }

                point.X += dir.X;
                point.Y += dir.Y;
            }

            return 0;
        }
        
        static int SumOfAdjacent(ref List<Tuple<Point, int>> data, int x, int y)
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

        static Point Coordinate(int input)
        {
            var result = new Point();

            var x = 0;
            var y = 0;
            var dx = 0;
            var dy = -1;
            
            for (var i = 0; i < input; i++)
            {
                result.X = x;
                result.Y = y;

                if ((x == y) || ((x < 0) && (x == -y)) || ((x > 0) && (x == 1 - y)))
                {
                    var z = dx;
                    dx = -dy;
                    dy = z;
                }
                x += dx;
                y += dy;
            }

            return result;
        }
    }
}
