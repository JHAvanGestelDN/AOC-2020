using System;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input =
                System.IO.File.ReadAllLines(@"C:\Users\admin\RiderProjects\AdventOfCode\Assets\12.1.txt");

            Console.WriteLine(PuzzleOne(input));
            Console.WriteLine(PuzzleTwo(input));
        }

        private static int PuzzleTwo(string[] input)
        {
            int northWaypoint = 1;
            int eastWaypoint = 10;

            int north = 0;
            int east = 0;

            foreach (string s in input)
            {
                int modifier = Convert.ToInt32(s.Substring(1));
                char instruction = s[0];

                if (instruction == 'F')
                {
                    north += modifier * northWaypoint;
                    east += modifier * eastWaypoint;
                }

                switch (instruction)
                {
                    case 'N':
                        northWaypoint += modifier;
                        break;
                    case 'S':
                        northWaypoint -= modifier;
                        break;
                    case 'E':
                        eastWaypoint += modifier;
                        break;
                    case 'W':
                        eastWaypoint -= modifier;
                        break;

                    case 'R':
                        Tuple<int, int> waypointsR = RotateWaypoints(northWaypoint, eastWaypoint, modifier, true);
                        northWaypoint = waypointsR.Item1;
                        eastWaypoint = waypointsR.Item2;
                        break;
                    case 'L':
                        Tuple<int, int> waypointsL = RotateWaypoints(northWaypoint, eastWaypoint, modifier, false);
                        northWaypoint = waypointsL.Item1;
                        eastWaypoint = waypointsL.Item2;
                        break;
                }
            }

            return Math.Abs(north) + Math.Abs(east);
        }

        private static int PuzzleOne(string[] input)
        {
            Direction current = Direction.Right;
            int north = 0;
            int east = 0;

            foreach (string s in input)
            {
                int modifier = Convert.ToInt32(s.Substring(1));
                char instruction = s[0];

                if (instruction == 'F')
                {
                    instruction = current switch
                    {
                        Direction.Up => 'N',
                        Direction.Right => 'E',
                        Direction.Down => 'S',
                        Direction.Left => 'W',
                        _ => instruction
                    };
                }

                switch (instruction)
                {
                    case 'N':
                        north += modifier;
                        break;
                    case 'S':
                        north -= modifier;
                        break;
                    case 'E':
                        east += modifier;
                        break;
                    case 'W':
                        east -= modifier;
                        break;

                    case 'R':
                        current = (Direction) DetermineDirection(current, modifier, true);
                        break;
                    case 'L':
                        current = (Direction) DetermineDirection(current, modifier, false);
                        break;
                }
            }

            return Math.Abs(north) + Math.Abs(east);
        }

        private static int DetermineDirection(Direction current, int rotation, bool right)
        {
            int x;
            if (right)
                x = (int) current + rotation;
            else
                x = (int) current - rotation;

            if (x > 360)
                x -= 360;
            if (x == 360)
                x = 0;
            if (x < 0)
                x += 360;
            return x;
        }

        private static Tuple<int, int> RotateWaypoints(int northW, int eastW, int rotation, bool right)
        {
            int newNorth = 0;
            int newEast = 0;

            switch (rotation)
            {
                case 90:
                    if (right)
                    {
                        newEast = northW;
                        newNorth = -1 * eastW;
                    }
                    else
                    {
                        newEast = -1*northW;
                        newNorth = eastW;
                    }
                    break;
                case 180:
                    newNorth = northW * -1;
                    newEast = eastW * -1;
                    break;
                case 270:
                    if (!right)
                    {
                        newEast = northW;
                        newNorth = -1 * eastW;
                    }
                    else
                    {
                        newEast = -1 * northW;
                        newNorth = eastW;
                    }
                    break;
            }

            return new Tuple<int, int>(newNorth, newEast);
        }
    }

    public enum Direction
    {
        Up = 0,

        Right = 90,

        Down = 180,

        Left = 270
    }
}