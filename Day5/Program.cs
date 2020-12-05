using System;
using System.Collections.Generic;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] txt =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\5.1.txt");

            int heighest = 0;
            List<int> seatIds = new List<int>();
            foreach (string s in txt)
            {
                int row = Get(s.Substring(0, 7), false);
                int col = Get(s.Substring(7, 3), true);
                int seatId = row * 8 + col;
                heighest = seatId > heighest ? seatId : heighest;

                seatIds.Add(seatId);
            }

            Console.WriteLine("Heighest seatId: " + heighest);
            Console.WriteLine("My seat: " + FindYourSeat(seatIds));
        }

        private static int? FindYourSeat(List<int> allSeats)
        {
            allSeats.Sort();
            int min = allSeats[0];

            for (int i = 1; i < allSeats.Count; i++)
            {
                if (allSeats[i] != min + 1)
                    return allSeats[i] - 1;
                min++;
            }

            return null;
        }

        private static int Get(string input, bool col)
        {
            int start = 0;
            int end = col ? 7 : 127;

            for (int i = 0; i < input.Length - 1; i++)
            {
                end = input[i] == 'F' || input[i] == 'L' ? end - (end - start) / 2 - 1 : end;
                start = input[i] == 'B' || input[i] == 'R' ? end - (end - start) / 2 : start;
            }

            return input[^1] == 'F' || input[^1] == 'L' ? start : end;
        }
    }
}