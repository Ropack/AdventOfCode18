using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fabric = new int[1000, 1000];
            using (var streamReader = new StreamReader("input.txt"))
            {
                string s;
                var list = new List<SquareDTO>();
                while (!string.IsNullOrWhiteSpace(s = streamReader.ReadLine()))
                {
                    var ss = s.Split('@');
                    var id = ss[0].Trim();
                    var ss2 = ss[1].Split(':');
                    var pos = ss2[0].Trim().Split(',');
                    var size = ss2[1].Trim().Split('x');
                    var posX = Convert.ToInt32(pos[0]);
                    var posY = Convert.ToInt32(pos[1]);
                    var sizeX = Convert.ToInt32(size[0]);
                    var sizeY = Convert.ToInt32(size[1]);
                    var notOverlapping = true;
                    for (int i = posX; i < posX + sizeX; i++)
                    {
                        for (int j = posY; j < posY + sizeY; j++)
                        {
                            if (fabric[i, j] != 0)
                            {
                                notOverlapping = false;
                            }
                            fabric[i, j]++;
                        }
                    }

                    if (notOverlapping)
                    {
                        list.Add(new SquareDTO()
                        {
                            Id = id,
                            PosX = posX,
                            PosY = posY,
                            SizeX = sizeX,
                            SizeY = sizeY
                        });
                    }
                }

                foreach (var squareDTO in list)
                {
                    var notOverlapping = true;
                    for (int i = squareDTO.PosX; i < squareDTO.PosX + squareDTO.SizeX; i++)
                    {
                        for (int j = squareDTO.PosY; j < squareDTO.PosY + squareDTO.SizeY; j++)
                        {
                            if (fabric[i, j] != 1)
                            {
                                notOverlapping = false;
                            }
                        }
                    }

                    if (notOverlapping)
                    {
                        Console.WriteLine(squareDTO.Id);
                    }
                }
                var sum = 0;
                foreach (var f in fabric)
                {
                    if (f > 1)
                    {
                        sum++;
                    }
                }
                Console.WriteLine(sum);
            }
        }
    }
}
