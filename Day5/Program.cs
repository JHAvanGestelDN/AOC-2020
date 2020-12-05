using System;
using System.Collections.Generic;
using System.Linq;

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
                if (seatId > heighest)
                    heighest = seatId;
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
            int end = 127;
            if (col)
                end = 7;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (!col && input[i] == 'F' || col && input[i] == 'L')
                    end = end - (end - start) / 2 - 1;
                if (!col && input[i] == 'B' || col && input[i] == 'R')
                {
                    start = end - (end - start) / 2;
                }
            }

            if (!col && input[^1] == 'F' || col && input[^1] == 'L')
                return start;
            return end;
        }
    }
}