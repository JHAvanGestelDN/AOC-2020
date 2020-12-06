using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input =
                System.IO.File.ReadAllText(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\6.1.txt");
            string[] groups = input.Split(new[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(PuzzleOne(groups));
            Console.WriteLine(PuzzleTwo(groups));
        }

        private static int PuzzleOne(string[] groups)
        {
            return groups.Select(s => new HashSet<char>(s.Replace(Environment.NewLine, ""))).Select(x => x.Count).Sum();
        }

        private static int PuzzleTwo(string[] groups)
        {
            int result = 0;

            foreach (string s in groups)
            {
                Dictionary<char, int> mapping = new Dictionary<char, int>();
                foreach (char c in s.Replace(Environment.NewLine + Environment.NewLine, ""))
                {
                    if (mapping.ContainsKey(c))
                        mapping[c]++;
                    else
                        mapping.Add(c, 1);
                }

                result += mapping.Count(v => v.Value >= s.Split('\n').Length);
            }

            return result;
        }
    }
}