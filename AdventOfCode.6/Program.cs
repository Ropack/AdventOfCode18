using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode._6
{
    class Program
    {
        static List<Point> points = new List<Point>();
        static int[,] arr = new int[360, 360];
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader("input.txt"))
            {

                string s;
                while (!string.IsNullOrWhiteSpace(s = streamReader.ReadLine()))
                {
                    var ss = s.Split(',');
                    points.Add(new Point(Convert.ToInt32(ss[0]), Convert.ToInt32(ss[1].Trim())));

                }

                for (int i = 0; i < 360; i++)
                {
                    for (int j = 0; j < 360; j++)
                    {
                        arr[i, j] = GetPointOwner(new Point(i, j));
                    }
                    //Console.WriteLine();
                }

                var finites = new List<int>();
                for (int i = 0; i < points.Count; i++)
                {
                    if (IsFinite(i))
                    {
                        finites.Add(i);
                    }
                }

                var maxLeft = 0;
                var maxRight = 0;
                var maxTop = 0;
                var maxBottom = 0;
                var max = 0;
                foreach (var finite in finites)
                {

                    //left
                    var left = 0;
                    var y = points[finite].Y;
                    var border = arr[left, y];
                    while (border == finite)
                    {
                        border = GetPointOwner(new Point(--left, y));
                    }
                    //top
                    var x = points[finite].X;
                    var top = 0;
                    border = arr[x, top];
                    while (border == finite)
                    {
                        border = GetPointOwner(new Point(x, --top));
                    }
                    //right
                    var right = 359;
                    y = points[finite].Y;
                    border = arr[right, y];
                    while (border == finite)
                    {
                        border = GetPointOwner(new Point(++right, y));
                    }
                    //bottom
                    x = points[finite].X;
                    var bottom = 359;
                    border = arr[x, bottom];
                    while (border == finite)
                    {
                        border = GetPointOwner(new Point(x, ++bottom));
                    }

                    if (maxLeft < left)
                    {
                        maxLeft = left;
                    }
                    if (maxTop < top)
                    {
                        maxTop = top;
                    }
                    if (maxRight < right)
                    {
                        maxRight = right;
                    }
                    if (maxBottom < bottom)
                    {
                        maxBottom = bottom;
                    }
                }


                var width = maxLeft * -1 + maxRight + 1;
                var height = maxTop * -1 + maxBottom + 1;
                var arr2 = new int[width, height];

                //left strip
                for (int i = 0; i < maxLeft * -1; i++)
                {
                    for (int j = 0; j < maxTop * -1 + maxBottom + 1; j++)
                    {
                        arr2[i, j] = GetPointOwner(new Point(i, j));
                    }
                }
                //right strip
                for (int i = maxLeft * -1 + 360; i < width; i++)
                {
                    for (int j = 0; j < maxTop * -1 + maxBottom + 1; j++)
                    {
                        arr2[i, j] = GetPointOwner(new Point(i, j));
                    }
                }
                //top middle
                for (int i = maxLeft * -1; i < maxLeft * -1 + 360; i++)
                {
                    for (int j = 0; j < maxTop * -1; j++)
                    {
                        arr2[i, j] = GetPointOwner(new Point(i, j));
                    }
                }
                //bottom middle
                for (int i = maxLeft * -1; i < maxLeft * -1 + 360; i++)
                {
                    for (int j = maxTop * -1 + 360; j < height; j++)
                    {
                        arr2[i, j] = GetPointOwner(new Point(i, j));
                    }
                }
                //middle
                for (int i = maxLeft * -1; i < maxLeft * -1 + 360; i++)
                {
                    for (int j = maxTop * -1; j < maxTop * -1 + 360; j++)
                    {
                        arr2[i, j] = arr[i - maxLeft * -1, j - maxTop * -1];
                    }
                }

                foreach (var finite in finites)
                {
                    var sum = 0;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (arr2[i, j] == finite)
                            {
                                sum++;
                            }
                        }
                    }

                    if (max < sum)
                    {
                        max = sum;
                    }
                }
                Console.WriteLine(max);
                var numOfPoints = 0;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        var sum = 0;
                        foreach (var point in points)
                        {
                            sum += GetManhattanDistance(point, new Point(i, j));
                        }

                        if (sum < 10000)
                        {
                            numOfPoints++;
                        }
                    }
                }
                Console.WriteLine(numOfPoints);
            }

        }

        private static int GetPointOwner(Point point)
        {
            var distances = new List<int>();
            foreach (var vector in points)
            {
                distances.Add(GetManhattanDistance(vector, point));
            }

            var min = distances.Min();
            if (distances.Count(d => d == min) > 1)
            {
                return -1;
            }
            else
            {
                return distances.IndexOf(min);
                //Console.Write(arr[i, j] + " ");
            }
        }

        static int GetManhattanDistance(Point point1, Point point2)
        {
            return (Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y));
        }

        static bool IsFinite(int pointIndex)
        {
            var v = points[pointIndex];
            var borderedRight = false;
            var borderedLeft = false;
            var borderedTop = false;
            var borderedBottom = false;

            // x
            for (int i = 0; i < points.Count; i++)
            {
                if (i != pointIndex)
                {
                    if (arr[points[i].X, v.Y] != pointIndex)
                    {
                        if (points[i].X > v.X)
                        {
                            borderedRight = true;
                        }
                        else
                        {
                            borderedLeft = true;
                        }
                    }

                    if (borderedRight && borderedLeft)
                    {
                        break;
                    }
                }
            }

            // y
            for (int i = 0; i < points.Count; i++)
            {
                if (i != pointIndex)
                {
                    if (arr[v.X, points[i].Y] != pointIndex)
                    {
                        if (points[i].Y > v.Y)
                        {
                            borderedTop = true;
                        }
                        else
                        {
                            borderedBottom = true;
                        }
                    }

                    if (borderedTop && borderedBottom)
                    {
                        break;
                    }
                }
            }

            if (!borderedBottom || !borderedRight || !borderedLeft || !borderedTop)
            {
                return false;
            }
            return true;
        }
    }
}
