namespace Day9
{
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("c:\\temp\\input.txt");
            
            Test("{}", 1, 0);
            Test("{{{}}}", 6, 0);
            Test("{{},{}}", 5, 0);
            Test("{{{},{},{{}}}}", 16, 0);
            Test("{<a>,<a>,<a>,<a>}", 1, 4);
            Test("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9, 8);
            Test("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9, 0);
            Test("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3, 17);

            var answer = Parse(input);
        }
        
        static int[] Parse(string input)
        {
            var current = 0;
            var count = 0;
            var garbage = false;
            var negating = false;
            var garbaged = 0;

            foreach (var chr in input)
            {
                if (garbage)
                {
                    switch (chr)
                    {
                        case '>':
                            garbage = negating;
                            if (!negating) continue;
                            break;
                        case '!':
                            negating = !negating;
                            continue;
                    }
                    if (!negating) garbaged++;
                    negating = false;
                }
                else
                {
                    switch (chr)
                    {
                        case '{':
                            current++;
                            break;
                        case '}':
                            count += current;
                            current--;
                            break;
                        case '<':
                            garbage = true;
                            break;
                    }
                }
            }

            return new [] {count,garbaged};
        }

        static void Test(string stream, int total, int garbaged)
        {
            var test = Parse(stream);
            if (test[0] != total && test[1] == garbaged) throw new Exception($"Total should be: {total} but was {test[0]}, Garbaged should be: {garbaged} but was {test[1]}");
        }
    }
}
