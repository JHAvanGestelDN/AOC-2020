using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\7.1.txt");

            List<Bag> rules = input.Select(s => new Bag(s.Split("contain "))).ToList();

            Console.WriteLine(PuzzelOne(rules));
            Console.WriteLine(PuzzelTwo(rules));
        }

        private static int PuzzelOne(List<Bag> rules)
        {
            List<Bag> bagsContainingBag = FindBagsThatContain(rules, "shiny gold bag");
            List<Bag> processedBags = new List<Bag>();
            while (bagsContainingBag.Count != 0)
            {
                List<Bag> tempList = FindBagsThatContain(rules, bagsContainingBag[0].Name);
                foreach (var bag in tempList.Where(bag => !processedBags.Contains(bag) && !bagsContainingBag.Contains(bag)))
                    bagsContainingBag.Add(bag);

                processedBags.Add(bagsContainingBag[0]);
                bagsContainingBag.Remove(bagsContainingBag[0]);
            }

            return processedBags.Count;
        }

        private static int PuzzelTwo(List<Bag> rules)
        {
            Bag myBag = rules.FirstOrDefault(r => r.Name.Contains("shiny gold bag"));
            return GetContains(myBag, rules) - 1;
        }

        private static int GetContains(Bag bag, List<Bag> rules)
        {
            if (bag.Contains.Count == 0)
                return 1;
            int result = 1 + bag.Contains.Sum(containingBag => 
                GetContains(rules.FirstOrDefault(r => r.Name.Contains(containingBag.Key)), rules) * containingBag.Value);
            
            return result;
        }

        private static List<Bag> FindBagsThatContain(List<Bag> rules, string bagName)
        {
            return (from bag in rules from keyValuePair in bag.Contains where keyValuePair.Key.Contains(bagName.Trim()) select bag).ToList();
        }
    }

    internal class Bag
    {
        public string Name { get;}
        public Dictionary<string, int> Contains { get; } = new();

        public Bag(string[] bag)
        {
            Name = bag[0].Trim();
            Addbag(bag[1]);
        }

        private void Addbag(string bag)
        {
            var split = bag.Replace(".", string.Empty).Split(", ");
            foreach (string s in split)
            {
                if (s == "no other bags") continue;
                int numberOfBags = Convert.ToInt32(s.Substring(0, 1).Trim());
                string bagname = numberOfBags==1?s.Substring(2).Trim()+"s": s.Substring(2).Trim();
                Contains.Add(bagname, numberOfBags);
            }
        }
    }
}