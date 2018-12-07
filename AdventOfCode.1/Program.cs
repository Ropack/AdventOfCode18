using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._1
{
    class Program
    {
        static void Main(string[] args)
        {

            var partResults = new List<int>();
            string s;
            var sum = 0;
            while (true)
            {
                using (var streamReader = new StreamReader("input.txt"))
                {
                    while (!string.IsNullOrEmpty(s = streamReader.ReadLine()))
                    {
                        sum += Convert.ToInt32(s);
                        if (partResults.Contains(sum))
                        {
                            Console.WriteLine($"First repeating frequency is: {sum}");
                            return;
                        }
                        partResults.Add(sum);
                    }
                    Console.WriteLine($"Result frequency is: {sum}");
                }

            }
        }
    }
}
