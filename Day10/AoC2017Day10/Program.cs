namespace AoC2017Day10
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            Test(string.Empty, "a2582a3a0e66e6e86e3812dcb672a272");
            Test("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd");
            Test("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d");
            Test("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e");

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)?.Replace("\\AoC2017Day10\\bin\\Debug", string.Empty);
            var answer = Hash(File.ReadAllText($"{path}\\input.txt").Replace("\n", string.Empty));
        } 

        static string Hash(string input)
        {
            var buffer = new int[256];
            var position = 0;
            var skip = 0;
            
            for (var i = 0; i < 256; i++)
                buffer[i] = i;
            
            for (var i = 1; i <= 64; i++)
                foreach (var ascii in input
                                        .Select((Convert.ToInt32))
                                            .Concat(new[] { 17, 31, 73, 47, 23 }))
                    Cycle(ref buffer, ref position, ascii, ref skip);

            return DenseHash(ref buffer);
        }
        
        static void Cycle(ref int[] buffer, ref int position, int length, ref int skip)
        {          
            if (length > 1)
                Write(ref buffer, position, length, Read(ref buffer, position, length).Reverse().ToArray());
            
            var incrementing = true;
            var count = 0;

            while (incrementing)
            {
                position++;
                count++;

                if (position > (buffer.Length - 1)) position = 0;
                if (count >= (length + skip)) incrementing = false;
            }
            
            skip++;
        }

        static int[] Read(ref int[] buffer, int bufferIndex, int length)
        {
            var slice = new int[length];
            var sliceIndex = 0;

            while (true)
            {
                slice[sliceIndex] = buffer[bufferIndex];
                bufferIndex++;
                sliceIndex++;

                if (sliceIndex > (length - 1)) break;
                if (bufferIndex > (buffer.Length - 1)) bufferIndex = 0;
            }
            
            return slice;
        }

        static void Write(ref int[] buffer, int bufferIndex, int length, IReadOnlyList<int> slice)
        {
            var sliceIndex = 0;

            while (true)
            {
                buffer[bufferIndex] = slice[sliceIndex];
                bufferIndex++;
                sliceIndex++;

                if (sliceIndex > (length - 1)) break;
                if (bufferIndex > (buffer.Length - 1)) bufferIndex = 0;
            }
        }

        static string DenseHash(ref int[] buffer)
        {
            var hash = new int[16];

            for (var i = 0; i < 16; i++)
                foreach (var ascii in new ArraySegment<int>(buffer, i * 16, 16))
                    hash[i] ^= ascii;
            
            return hash
                    .Aggregate(string.Empty, (a, b) => a + b.ToString("X2"))
                        .ToLower();
        }

        static void Test(string input, string hash)
        {
            var result = Hash(input);
            if (result != hash)
                throw new Exception($"Input was {input} but was expecting hash to be {hash} but was {result}");
        }
    }
}
