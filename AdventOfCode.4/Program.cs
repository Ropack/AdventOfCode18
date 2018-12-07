using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader("input.txt"))
            {
                var entries = new List<Entry>();
                string s;
                while (!string.IsNullOrEmpty(s = streamReader.ReadLine()))
                {
                    var strings = s.Split(']');
                    var date = strings[0].Replace("[", "");

                    entries.Add(new Entry()
                    {
                        DateTime = Convert.ToDateTime(date),
                        Message = strings[1].Trim()
                    });
                }

                entries = entries.OrderBy(e => e.DateTime).ToList();
                var guards = new List<Guard>();
                var sleepStart = DateTime.MaxValue;
                Guard actGuard = null;
                foreach (var entry in entries)
                {
                    if (entry.Message.StartsWith("Guard"))
                    {
                        var id = Convert.ToInt32(entry.Message.Split(' ')[1].Replace("#", ""));
                        if (guards.Any(g => g.Id == id))
                        {
                            actGuard = guards.Single(g => g.Id == id);
                        }
                        else
                        {
                            guards.Add(new Guard()
                            {
                                Id = id,
                                MinutesAsleep = new int[60]
                            });
                            actGuard = guards.Last();
                        }
                    }
                    else if (entry.Message == "falls asleep")
                    {
                        sleepStart = entry.DateTime;
                    }
                    else if (entry.Message == "wakes up")
                    {
                        var diff = sleepStart - entry.DateTime;
                        for (int i = sleepStart.Minute; i < entry.DateTime.Minute; i++)
                        {
                            actGuard.MinutesAsleep[i]++;
                        }
                    }
                }

                Guard maxGuard = null;
                int maxMinutes = 0;
                foreach (var guard in guards)
                {
                    var sum = 0;
                    foreach (var i in guard.MinutesAsleep)
                    {
                        sum += i;
                    }

                    if (maxMinutes < sum)
                    {
                        maxMinutes = sum;
                        maxGuard = guard;
                    }
                }

                Console.WriteLine(Array.IndexOf(maxGuard.MinutesAsleep, maxGuard.MinutesAsleep.Max()) * maxGuard.Id);
                var guard2 = guards.OrderByDescending(g => g.MinutesAsleep.Max()).First();
                
                Console.WriteLine(Array.IndexOf(guard2.MinutesAsleep, guard2.MinutesAsleep.Max()) * guard2.Id);
            }
        }
    }
}
