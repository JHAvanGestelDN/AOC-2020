using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\11.1.txt");
            Console.WriteLine(StoelenDans(input,4,1));
            Console.WriteLine(StoelenDans(input,5,2));
            
        }

        private static int StoelenDans(string[] input, int tolorant, int puzzle)
        {
            string[] copy = new string[input.Length];
            Array.Copy(input, copy, input.Length);
            bool change = true;
            while (change)
            {
                change = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        Tuple<int, int> position = new Tuple<int, int>(i, j);
                        string neighbours = puzzle==1? GetNeighbours(input, position): GetSeeingNeighbour(input,position);
                        StringBuilder sb = new StringBuilder(copy[position.Item1]);

                        switch (copy[position.Item1][position.Item2])
                        {
                            case 'L' when neighbours.All(x => x != '#'):
                                change = true;
                                sb[position.Item2] = '#';
                                copy[position.Item1] = sb.ToString();
                                break;
                            case '#' when neighbours.Count(x => x == '#') >= tolorant:
                                change = true;
                                sb[position.Item2] = 'L';
                                copy[position.Item1] = sb.ToString();
                                break;
                        }
                    }
                }

                Array.Copy(copy, input, input.Length);
            }
            return copy.Sum(s => s.Count(c => c == '#'));
        }

        private static string GetNeighbours(string[] input, Tuple<int, int> position)
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();
            int row = position.Item1;
            int col = position.Item2;

            //up
            if (row > 0)
                neighbours.Add(new Tuple<int, int>(row - 1, col));
            //upright
            if (row > 0 && col < input[row].Length - 1)
                neighbours.Add(new Tuple<int, int>(row - 1, col + 1));
            //right
            if (col < input[row].Length - 1)
                neighbours.Add(new Tuple<int, int>(row, col + 1));
            //downright
            if (row < input.Length - 1 && col < input[row].Length - 1)
                neighbours.Add(new Tuple<int, int>(row + 1, col + 1));
            //down
            if (row < input.Length - 1)
                neighbours.Add(new Tuple<int, int>(row + 1, col));
            //downleft
            if (row < input.Length - 1 && col > 0)
                neighbours.Add(new Tuple<int, int>(row + 1, col - 1));
            //left
            if (col > 0)
                neighbours.Add(new Tuple<int, int>(row, col - 1));
            //upleft
            if (row > 0 && col > 0)
                neighbours.Add(new Tuple<int, int>(row - 1, col - 1));

            return neighbours.Aggregate("", (current, neighbour) => current + input[neighbour.Item1][neighbour.Item2]);
        }

        public static string GetSeeingNeighbour(string[] input, Tuple<int, int> initPosition)
        {
            List<char> neigbours = new List<char>();
            foreach (Direction dir in (Direction[]) Enum.GetValues(typeof(Direction)))
            {
                Tuple<int, int> position = initPosition;
                char neighbour = '.';
                while (neighbour == '.')
                {
                    int row = position.Item1;
                    int col = position.Item2;
                    switch (dir)
                    {
                        //up
                        case Direction.Up:
                            if (row > 0)
                            {
                                row--;
                            }

                            break;
                        //upright
                        case Direction.Upright:
                            if (row > 0 && col < input[row].Length - 1)
                            {
                                row--;
                                col++;
                            }

                            break;

                        //right

                        case Direction.Right:
                            if (col < input[row].Length - 1)
                            {
                                col++;
                            }

                            break;

                        //downright
                        case Direction.Downright:
                            if (row < input.Length - 1 && col < input[row].Length - 1)
                            {
                                row++;
                                col++;
                            }

                            break;

                        //down
                        case Direction.Down:
                            if (row < input.Length - 1)
                            {
                                row++;
                            }

                            break;

                        //downleft
                        case Direction.Downleft:
                            if (row < input.Length - 1 && col > 0)
                            {
                                row++;
                                col--;
                            }

                            break;

                        //left
                        case Direction.Left:
                            if (col > 0)
                            {
                                col--;
                            }

                            break;

                        //upleft
                        case Direction.Upleft:
                            if (row > 0 && col > 0)
                            {
                                row--;
                                col--;
                            }

                            break;
                    }

                    if (row == position.Item1 && col == position.Item2)
                        break;
                    position = new Tuple<int, int>(row, col);
                    neighbour = input[position.Item1][position.Item2];
                }

                neigbours.Add(neighbour);
            }

            return neigbours.Where(n => n != '.').ToList().Aggregate("", (current, c) => current + c);
        }
    }

    public enum Direction
    {
        Up,
        Upright,
        Right,
        Downright,
        Down,
        Downleft,
        Left,
        Upleft
    }
}