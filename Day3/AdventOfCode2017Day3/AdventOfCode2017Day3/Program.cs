using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day3
{
    using System.Drawing;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            var test = ValueLargerThanInput(289326);
        }
        
        static int ValueLargerThanInput(int input)
        {
            var data = new List<Tuple<int, int, int>>();
            
            var x = 0;
            var y = 0;
            var dx = 0;
            var dy = -1;

            for (var i = 0; i < input; i++)
            {
                var sum = SumOfAdjacent(ref data, x, y);

                if (sum > input) return sum;

                data.Add(new Tuple<int, int, int>(x, y, sum));
                
                if ((x == y) || ((x < 0) && (x == -y)) || ((x > 0) && (x == 1 - y)))
                {
                    var z = dx;
                    dx = -dy;
                    dy = z;
                }
                x += dx;
                y += dy;
            }

            return 0;
        }
        
        static int SumOfAdjacent(ref List<Tuple<int, int, int>> data, int x, int y)
        {
            var result = data.FirstOrDefault(p => p.Item1 == x - 1 && p.Item2 == y - 1)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x && p.Item2 == y - 1)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x + 1 && p.Item2 == y - 1)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x - 1 && p.Item2 == y)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x + 1 && p.Item2 == y)?.Item3 ?? 0;
    
            result += data.FirstOrDefault(p => p.Item1 == x - 1 && p.Item2 == y + 1)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x && p.Item2 == y + 1)?.Item3 ?? 0;
            result += data.FirstOrDefault(p => p.Item1 == x + 1 && p.Item2 == y + 1)?.Item3 ?? 0;

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
