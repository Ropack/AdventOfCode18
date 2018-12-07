using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._5
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader("input.txt"))
            {
                var origS = streamReader.ReadLine();
                var s = new string(origS);
                var doubles = new List<string>();
                for (int i = 65; i < 91; i++)
                {
                    doubles.Add(new string(new[] { (char)i, (char)(i + 32) }));
                    doubles.Add(new string(new[] { (char)(i + 32), (char)i }));
                }

                s = React(doubles, s);

                Console.WriteLine(s.Length);
                var polymers = new List<int>();
                for (int i = 65; i < 91; i++)
                {
                    var ds = origS.Replace(((char) i).ToString(), "").Replace(((char) (i + 32)).ToString(), "");
                    polymers.Add(React(doubles, ds).Length);
                }
                Console.WriteLine(polymers.Min());
            }
        }

        static string React(List<string> doubles, string s)
        {

            var go = true;
            while (go)
            {
                var beginLength = s.Length;
                foreach (var d in doubles)
                {
                    s = s.Replace(d, "");
                }

                if (beginLength == s.Length)
                {
                    go = false;
                }
            }

            return s;
        }
    }
}
