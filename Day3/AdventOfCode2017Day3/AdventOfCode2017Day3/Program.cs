using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day3
{
    using System.Drawing;

    class Program
    {
        static void Main(string[] args)
        {
            var point = Coordinate(289326);
            var test = Distance(0, 0, point.X, point.Y);
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
