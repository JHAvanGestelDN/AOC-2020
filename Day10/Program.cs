using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\10.s.txt");
            List<int> numbers = Array.ConvertAll(input, s => Convert.ToInt32(s)).ToList();
            numbers.Sort();

            Console.WriteLine(PuzzleOne(numbers));
            Console.WriteLine(PuzzleTwo(numbers));
        }

        public static int PuzzleOne(List<int> numbers)
        {
            Dictionary<int, int> kv = new Dictionary<int, int>();
            kv.Add(numbers[0], numbers[0]);
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                int current = numbers[i];
                int next = numbers[i + 1];
                int dif = next - current;
                if (kv.ContainsKey(dif))
                    kv[dif]++;
                else
                    kv.Add(dif, 1);
            }

            kv[3]++; // dif between built-in adapter and highest adapter  
            return kv[1] * kv[3];
        }

        public static long PuzzleTwo(List<int> numbers)
        {
            numbers.Insert(0,0);
            numbers.Add(numbers.Last()+3);

            Dictionary<int, long> dictionary = new Dictionary<int, long> {[numbers.Count - 1] = 1};

            for (int i = numbers.Count - 2; i >= 0; i--) 
            {
                long temp = 0;
                for (int j = i + 1; j < numbers.Count && numbers[j] - numbers[i] <= 3; j++)
                    temp += dictionary[j];

                dictionary[i] = temp;
            }

            return dictionary[0];
        }
    }
}