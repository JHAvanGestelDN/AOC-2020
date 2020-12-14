using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\13.1.txt");
            // Console.WriteLine(PuzzleOne(input));
            Console.WriteLine(PuzzleTwo(input[1].Split(',')));
        }

        public static int PuzzleOne(string[] input)
        {
            int earliestTime = Convert.ToInt32(input[0]);
            var busses = input[1].Split(',').Where(b => b != "x").ToList();

            int compareTo = Int32.MaxValue;
            int earliestBus = 0;
            foreach (string buss in busses)
            {
                int busNumber = Convert.ToInt32(buss);
                int dif = (((earliestTime / busNumber) + 1) * busNumber) - earliestTime;
                if (dif >= compareTo) continue;
                compareTo = dif;
                earliestBus = busNumber;
            }

            return (compareTo * earliestBus);
        }

        public static long PuzzleTwo(string[] input)
        {
            
            List<(int, int)> busOffset = new List<(int, int)>();
            for(int i=0;i<input.Length;i++)
            {
                if(input[i]!="x")
                    busOffset.Add((Convert.ToInt32(input[i]),i));
            }

            long iterator = busOffset[0].Item1; 
            long time = iterator; 

            foreach ((int, int) tuple in busOffset.Skip(1)) 
            {
                while ((time+tuple.Item2) %tuple.Item1!=0) 
                {
                    time = checked(time + iterator); 
                }

                iterator *= tuple.Item1; 
            }
            
            return time;
        }
    }
    
}