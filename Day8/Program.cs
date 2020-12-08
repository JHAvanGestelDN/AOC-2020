using System;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\8.1.txt");
            Console.WriteLine(PuzzleOne(input));
            Console.WriteLine(PuzzleTwo(input));
        }

        private static int PuzzleOne(string[] input)
        {
            return EscapeAttempt(input).Item2;
        }

        private static int? PuzzleTwo(string[] input)
        {
            string[] modInput = new string[input.Length];

            Tuple<bool, int> escapeAble = EscapeAttempt(input); //escapeable on without modification? - Unlikely
            if (escapeAble.Item1) return escapeAble.Item2; 

            for (int i = 0; i < input.Length; i++)
            {
                string[] instruction = input[i].Trim().Split(" ");
                if (instruction[0] == "acc") continue;
                Array.Copy(input, modInput, input.Length);

                modInput[i] = instruction[0] == "nop" ? "jmp " + instruction[1] : "nop " + instruction[1];

                escapeAble = EscapeAttempt(modInput);
                if (escapeAble.Item1)
                    return escapeAble.Item2;
            }
            return null;
        }

        private static int Helper(string instruction)
        {
            return instruction.Substring(0, 1) == "+"
                ? Convert.ToInt32(instruction.Substring(1))
                : Convert.ToInt32(instruction.Substring(1)) * -1;
        }

        private static Tuple<bool, int> EscapeAttempt(string[] modInstructions)
        {
            HashSet<int> indexes = new HashSet<int>();
            int acc = 0;
            for (int i = 0; i < modInstructions.Length; i++)
            {
                if (indexes.Contains(i))
                {
                    return new Tuple<bool, int>(false, acc);
                }

                indexes.Add(i);
                string[] instruction = modInstructions[i].Trim().Split(" ");
                switch (instruction[0])
                {
                    case "nop":
                        break;
                    case "acc":
                        acc += Helper(instruction[1]);
                        break;
                    case "jmp":
                        i += Helper(instruction[1]);
                        i--;
                        break;
                }
            }

            return new Tuple<bool, int>(true, acc);
        }
    }
}