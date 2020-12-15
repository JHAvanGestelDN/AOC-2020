using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day14
{
    class Program
    {
        private const int BitSize = 36;

        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\14.1.txt");
            Console.WriteLine(PuzzleOne(input));
            Console.WriteLine(PuzzleTwo(input));
        }

        private static long PuzzleOne(string[] input)
        {
            string mask = input[0].Replace("mask = ", String.Empty);

            Dictionary<long, long> dic = new Dictionary<long, long>();
            foreach (string s in input.Skip(1))
            {
                if (s.Contains("mask"))
                    mask = s.Replace("mask = ", String.Empty);
                else
                {
                    string[] split = s.Split("] = ");
                    long value = ProcesNumberP1(mask, Convert.ToString(Convert.ToInt64(split[1]), 2));
                    int location = Convert.ToInt32(split[0].Replace("mem[", String.Empty));
                    dic[location] = value;
                }
            }

            return dic.Sum(keyValuePair => keyValuePair.Value);
        }

        private static long ProcesNumberP1(string mask, string number)
        {
            number = AddPreamble(BitSize, '0', number);

            char[] numberC = number.ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] != 'X')
                    numberC[i] = mask[i];
            }

            number = new string(numberC);
            return Convert.ToInt64(number, 2);
        }

        private static long PuzzleTwo(string[] input)
        {
            string mask = input[0].Replace("mask = ", String.Empty);

            Dictionary<long, int> dic = new Dictionary<long, int>();
            foreach (string s in input.Skip(1))
            {
                if (s.Contains("mask"))
                    mask = s.Replace("mask = ", String.Empty);
                else
                {
                    string[] split = s.Split("] = ");
                    int number = Convert.ToInt32(split[0].Replace("mem[", String.Empty));
                    IEnumerable<long> addresses = ProcesNumberP2(mask, Convert.ToString(number, 2));
                    int value = Convert.ToInt32(split[1]);

                    foreach (long address in addresses)
                    {
                        dic[address] = value;
                    }
                }
            }

            return dic.Aggregate<KeyValuePair<long, int>, long>(0,
                (current, keyValuePair) => current + keyValuePair.Value);
        }

        private static IEnumerable<long> ProcesNumberP2(string mask, string firstMemAdres)
        {
            firstMemAdres = AddPreamble(BitSize, '0', firstMemAdres);

            //replace bits in first memory adres
            char[] charArray = firstMemAdres.ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X' || mask[i] == '1')
                    charArray[i] = mask[i];
            }

            //create a string with the maximum number of permutations (based on bits)
            string binary = "";
            int numberOfX = charArray.Count(c => c == 'X');
            for (int i = 0; i < numberOfX; i++)
            {
                binary += '1';
            }

            int numberOfPossibleCombinations = Convert.ToInt32(binary, 2) + 1; // add one to account for bit  0

            //create a list of all binary posibble combinations
            List<string> binaryOfNumberOfCombinations = new List<string>();
            for (int i = 0; i < numberOfPossibleCombinations; i++)
            {
                string binCurrentCom = Convert.ToString(i, 2);
                binCurrentCom = AddPreamble(numberOfX, '0', binCurrentCom);
                binaryOfNumberOfCombinations.Add(binCurrentCom);
            }

            //create permuations (each permutation = adres)
            List<long> memoryAdresses = new List<long>();
            foreach (string binaryOfNumberOfCombination in binaryOfNumberOfCombinations)
            {
                char[] copy = new char[charArray.Length];
                Array.Copy(charArray, copy, charArray.Length);

                int c = 0;
                for (var i = 0; i < copy.Length; i++)
                {
                    if (copy[i] != 'X') continue;
                    copy[i] = binaryOfNumberOfCombination[c];
                    c++;
                }

                string binaryAdress = new string(copy);

                memoryAdresses.Add(Convert.ToInt64(binaryAdress, 2));
            }

            return memoryAdresses;
        }

        private static string AddPreamble(int numberOf, char character, string originel)
        {
            if (originel.Length < numberOf)
            {
                string append = "";
                for (int i = 0; i < numberOf - originel.Length; i++)
                {
                    append += character;
                }

                originel = append + originel;
            }

            return originel;
        }
    }
}