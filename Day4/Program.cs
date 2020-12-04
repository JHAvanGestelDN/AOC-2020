using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string sample =
                System.IO.File.ReadAllText(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\4.1.txt");
            string[] passports = sample.Split(new[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            List<Passport> validPassports = new List<Passport>();

            foreach (string passport in passports)
            {
                string formattedPassport = passport.Replace(Environment.NewLine, " ");
                string[] passportIdentifiers = formattedPassport.Split(' ');
               
                if (passportIdentifiers.Length < 7) continue;
                Passport temp = CreatePassport(passportIdentifiers);
                if (temp != null)
                    validPassports.Add(temp);
            }

            Console.WriteLine();
            Console.WriteLine("Total count before checks");
            Console.WriteLine(validPassports.Count);

            int validAfterChecks = 0;
            List<Passport> reallyValidPassports = new List<Passport>();

            foreach (var passport in validPassports.Where(passport => passport.Valid))
            {
                validAfterChecks++;
                reallyValidPassports.Add(passport);
            }

            Console.WriteLine();
            Console.WriteLine("Total count after checks");
            Console.WriteLine(validAfterChecks);
        }

        public static Passport CreatePassport(string[] passportIdentifiers)
        {
            Passport passportObject = null;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (string passportIdentifier in passportIdentifiers)
            {
                var split = passportIdentifier.Split(':');
                keyValuePairs.Add(split[0], split[1]);
            }

            try
            {
                passportObject = new Passport(
                    keyValuePairs["byr"],
                    keyValuePairs["iyr"],
                    keyValuePairs["eyr"],
                    keyValuePairs["hgt"],
                    keyValuePairs["hcl"],
                    keyValuePairs["ecl"],
                    keyValuePairs["pid"]);

                string cid;
                if (keyValuePairs.TryGetValue("cid", out cid))
                    passportObject.Cid = cid;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in CreatePasswordMethod" + e);
                return passportObject;
            }

            return passportObject;
        }
    }

    internal class Passport
    {
        public int Byr { get; set; }
        public int Iyr { get; set; }
        public int Eyr { get; set; }
        public string Hgt { get; set; }
        public int Height { get; set; }
        public string HeightDiscr { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }

        public bool Valid { get; set; } = true;

        public Passport(string byr, string iyr, string eyr, string hgt, string hcl, string ecl, string pid)
        {
            Byr = Convert.ToInt32(byr);
            if (Byr < 1920 || Byr > 2002)
            {
                Valid = false;
                Console.WriteLine("Byr is invalid " + byr);
            }

            Iyr = Convert.ToInt32(iyr);
            if (Iyr < 2010 || Iyr > 2020)
            {
                Valid = false;
                Console.WriteLine("Iyr is invalid " + iyr);
            }

            Eyr = Convert.ToInt32(eyr);
            if (Eyr < 2020 || Eyr > 2030)
            {
                Valid = false;
                Console.WriteLine("Eyr is invalid " + eyr);
            }

            Hgt = hgt;
            Height = Convert.ToInt32(Hgt.Substring(0, hgt.Length - 2));
            HeightDiscr = Hgt.Substring(Hgt.Length - 2);
            if (Hgt.Substring(Hgt.Length - 2) == "cm")
            {
                if (Height < 150 || Height > 193)
                {
                    Valid = false;
                    Console.WriteLine("Hgt in cm is invalid" + hgt);
                }
            }
            else if (hgt.Substring(hgt.Length - 2) == "in")
            {
                if (Height < 59 || Height > 76)
                {
                    Valid = false;
                    Console.WriteLine("Hgt in inch is invalid " + hgt);
                }
            }

            Hcl = hcl;
            if (Hcl[0] != '#')
            {
                Valid = false;
                Console.WriteLine("hcl: " + hcl + "+ doesn't start with #");
            }
            else
            {
                char[] validCharacters = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
                for (int i = 1; i < Hcl.Length; i++)
                {
                    if (validCharacters.Contains(Hcl[i])) continue;
                    Valid = false;
                    Console.WriteLine("char " + Hcl[i] + " is invalid of Hcl: " + hcl);
                }
            }

            Ecl = ecl;
            if (!Enum.IsDefined(typeof(EyeColor), Ecl))
            {
                Valid = false;
                Console.WriteLine("ecl: " + ecl + " is not defined in enumlist");
            }

            Pid = pid;
            if (Pid.Length != 9)
            {
                Valid = false;
                Console.WriteLine("Pid lenght !=9 " + pid);
            }
            else
            {
                char[] validNumberCharacters = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
                foreach (char c in Pid)
                {
                    if (validNumberCharacters.Contains(c)) continue;
                    Valid = false;
                    Console.WriteLine("char " + c + " is invalid of pid: " + pid);
                }
            }
        }
    }

    internal enum EyeColor
    {
        amb,
        blu,
        brn,
        gry,
        grn,
        hzl,
        oth
    }
}