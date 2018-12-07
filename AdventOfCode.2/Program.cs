using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader("input.txt"))
            {
                var doubles = 0;
                var triples = 0;
                string s;
                var list = new List<string>();
                while (!string.IsNullOrEmpty(s = streamReader.ReadLine()))
                {
                    foreach (var a in list)
                    {
                        var copyA = new string(a);
                        var secCopyA = new string(a);
                        for (int i = 0; i < a.Length; i++)
                        {
                            var x = a.Remove(i, 1);
                            for (int j = 0; j < s.Length; j++)
                            {
                                var y = s.Remove(j, 1);
                                if (x == y)
                                {
                                    Console.WriteLine(x);
                                }
                            }    
                        }
                    }
                    list.Add(s);
                    var dictionary = new Dictionary<char, int>();
                    foreach (char c in s)
                    {
                        if (dictionary.ContainsKey(c))
                        {
                            dictionary[c] += 1;
                        }
                        else
                        {
                            dictionary.Add(c, 1);
                        }
                    }

                    var firstDouble = true;
                    var firstTriple = true;
                    foreach (var kvp in dictionary)
                    {
                        if (kvp.Value == 2 && firstDouble)
                        {
                            doubles++;
                            firstDouble = false;
                        }
                        else if (kvp.Value == 3 && firstTriple)
                        {
                            triples++;
                            firstTriple = false;
                        }
                    }
                }

                var sum = triples * doubles;
                Console.WriteLine($"Result checksum is: {sum}");

            }
        }
    }
}
