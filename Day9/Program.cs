using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\9.1.txt");
            long[] numbers = input.Select(long.Parse).ToArray();

            long answerPuzzleOne = PuzzleOne(numbers,25); //5 for sample, 25 for 9.1
            Console.WriteLine(answerPuzzleOne);
            Console.WriteLine(PuzzleTwo(numbers, answerPuzzleOne));
        }

        private static long PuzzleTwo(long[] numbers, long answerPuzzleOne)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                List<long> range = new List<long> {numbers[i]};
                long sum = numbers[i];

                int j = i+1;
                while (sum < answerPuzzleOne)
                {
                    sum += numbers[j];
                    range.Add(numbers[j]);
                    if (sum == answerPuzzleOne)
                    {
                        range.Sort();
                        return range.First() + range.Last();
                    }

                    j++;
                }
            }

            return 0;
        }

        private static long PuzzleOne(long[] numbers, int preamble)
        {
            int numberOfPreamble = preamble;
            for (int i = numberOfPreamble; i < numbers.Length; i++)
            {
                long combinedNumber = numbers[i];
                long[] subset = numbers.Skip(i - numberOfPreamble).Take(numberOfPreamble).ToArray();
                Array.Sort(subset);

                bool combineAble = false;
                for (int j = numberOfPreamble - 1; j > 0; j--)
                {
                    if (subset[j] < combinedNumber)
                        combineAble = FindSecond(subset, combinedNumber - subset[j]);

                    if (combineAble)
                        break;
                }

                if (!combineAble)
                    return combinedNumber;
            }

            return 0;
        }

        private static bool FindSecond(long[] numbers, long lookingFor)
        {
            bool found = false;
            foreach (long number in numbers)
            {
                if (number == lookingFor)
                    found = true;
            }

            return found;
        }
    }
}