using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Advent_of_code_2024.Utilities;

namespace AdventOfCode.Day2
{
    public class Day2
    {
        private bool checkIfSafe(int[] line)
        {


            if (line.Length < 2) return true;

            bool isIncreasing = line[1] > line[0];
            bool isSafe = true;

            for (int i = 1; i < line.Length; i++)
            {
                int difference = line[i] - line[i - 1];

                if (isIncreasing)
                {
                    if (difference <= 0 || difference > 3)
                    {
                        isSafe = false;
                        break;
                    }
                }
                else
                {
                    if (difference >= 0 || difference < -3)
                    {
                        isSafe = false;
                        break;
                    }
                }

            }

            return isSafe;
        }
        public void Run()
        {

            var input = Utility.GetInput(2);
            List<int[]> ints = new List<int[]>();
            
            foreach (string s in input)
            {
                int[] array = s.Split(" ").Select(Int32.Parse).ToArray();
                ints.Add(array);
            }
            
            int amountOfSafe = 0;

            foreach (int[] line in ints)
            {
                if (checkIfSafe(line))
                {
                    amountOfSafe += 1;
                }
            }
            
            Console.WriteLine($"Part 1 amount of safe: {amountOfSafe}");


            int amoutOfSafeIfOneIsAllowed = 0;
            foreach (int[] line in ints)
            {
                if (checkIfSafe(line) || Enumerable.Range(0, line.Length) //Get range of 0 to the length of the list of integers +1
                        //Take(i) gets the first i elements, Skip(i) skips the first i elements.
                        //This way we can iterate over every combination of 1 skipped element to check if any of them returns true.
                        .Any(i => checkIfSafe(line.Take(i).Concat(line.Skip(i + 1)).ToArray()))) 
                {
                    amoutOfSafeIfOneIsAllowed += 1;
                }
            }
            
            Console.WriteLine($"Part 2 amount of safe: {amoutOfSafeIfOneIsAllowed}");


        }
    }
}