using System;
using System.Diagnostics;
using System.Threading;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] inputSmall = new[]
            {
                1721,
                979,
                366,
                299,
                675,
                1456,
            };
            int[] inputLarge = new[]
            {
                1753,
                1858,
                1860,
                1978,
                1758,
                1847,
                2010,
                1679,
                1222,
                1723,
                1592,
                1992,
                1865,
                1635,
                1692,
                1653,
                1485,
                848,
                1301,
                1818,
                1872,
                1883,
                1464,
                2002,
                1736,
                1821,
                1851,
                1299,
                1627,
                1698,
                1713,
                1676,
                1673,
                1448,
                1939,
                1506,
                1896,
                1710,
                1677,
                1894,
                1645,
                1454,
                1972,
                1687,
                265,
                1923,
                1666,
                1761,
                1386,
                2006,
                1463,
                1759,
                1460,
                1722,
                1670,
                1731,
                1732,
                1976,
                1564,
                1380,
                1981,
                1998,
                1912,
                1479,
                1500,
                167,
                1904,
                1689,
                1810,
                1675,
                1811,
                1671,
                1535,
                1624,
                1638,
                1848,
                1646,
                1795,
                1717,
                1803,
                1867,
                1794,
                1774,
                1245,
                1915,
                1601,
                1656,
                1472,
                1700,
                1887,
                1869,
                1876,
                1561,
                1743,
                1900,
                1574,
                1400,
                1950,
                1893,
                1576,
                1903,
                1747,
                1560,
                1445,
                1652,
                633,
                1970,
                1812,
                1807,
                1788,
                1948,
                1588,
                1639,
                1719,
                1680,
                1773,
                1890,
                1347,
                1344,
                1456,
                1691,
                1842,
                1585,
                1953,
                410,
                1791,
                485,
                1412,
                1994,
                1799,
                1955,
                1554,
                1661,
                1708,
                1824,
                1553,
                1993,
                1911,
                1515,
                1545,
                856,
                1685,
                1982,
                1954,
                1480,
                1709,
                1428,
                1829,
                1606,
                1613,
                1941,
                1483,
                1513,
                1664,
                1801,
                1720,
                1984,
                1575,
                1805,
                1833,
                1418,
                1882,
                1746,
                483,
                1674,
                1467,
                1453,
                523,
                1414,
                1800,
                1403,
                1946,
                1868,
                1520,
                1861,
                1580,
                1995,
                1960,
                1625,
                1411,
                1558,
                1817,
                1854,
                1617,
                1478,
                735,
                1593,
                1778,
                1809,
                1584,
                1438,
                1845,
                1712,
                1655,
                1990,
                1578,
                1703,
                1895,
                1765,
                1572,
            };
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch.Start();
            int? resultTwo = TwoNumbers(inputLarge);
            stopwatch.Stop();
            stopwatch2.Start();
            int? resultThree = ThreeNumbers(inputLarge);
            stopwatch.Stop();
            
            
            Console.WriteLine(resultTwo != null ? resultTwo : "Geen resultaat");
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine(resultThree != null ? resultThree : "Geen resultaat");
            Console.WriteLine(stopwatch2.Elapsed);

            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            Thread.Sleep(1000);
            stopwatch3.Stop();
            Console.WriteLine(stopwatch3.Elapsed);

        }
        

        public static int? TwoNumbers(int[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                        return input[i] * input[j];
                }
            }

            return null;
        }

        public static int? ThreeNumbers(int[] input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                for (int j = 1; j < input.Length - 1; j++)
                {
                    for (int k = 2; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                            return input[i] * input[j]*input[k];
                    }
                }
            }

            return null;
        }
    }
}