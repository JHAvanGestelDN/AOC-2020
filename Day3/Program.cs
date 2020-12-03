using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sample =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\3.1.txt");

            int[] rowIncrease = {1, 1, 1, 1, 2};
            int[] colIncrease = {1, 3, 5, 7, 1};

            int result = 1;
            for (int i = 0; i < rowIncrease.Length; i++)
            {
                result *= CleaverMethodName(rowIncrease[i], colIncrease[i], sample);
            }

            Console.WriteLine(result);
        }

        private static int CleaverMethodName(int rowIncrease, int colIncrease, string[] input)
        {
            int trees = 0;
            int j = colIncrease;
            for (int i = rowIncrease; i < input.Length; i += rowIncrease)
            {
                if (j >= input[i].Length)
                    j = 0 + (j - input[i].Length);

                if (input[i][j] == '#')
                {
                    trees++;
                }

                j += colIncrease;
            }

            return trees;
        }
    }
}