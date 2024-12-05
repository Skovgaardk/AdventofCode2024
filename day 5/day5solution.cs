using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Advent_of_code_2024.Utilities;
using static System.Int32;

namespace AdventOfCode.Day5
{
    public class Day5
    {
        public void Run()
        {
            var input = Utility.GetInput(5);


            List<string> rules = new List<string>();
            var updates = new List<int[]>();

            var emptyIndex = Array.FindIndex(input, string.IsNullOrEmpty);

            rules = input.Take(emptyIndex).ToList();
            updates = input.Skip(emptyIndex + 1)
                .Select(x => x.Split(","))
                .Select(y => y.Select(Parse).ToArray())
                .ToList();

            Dictionary<int, List<int>> rulesAsDict = new Dictionary<int, List<int>>();
            
            foreach (string v in rules)
            {
                var split = v.Split("|");
                int key = Parse(split[0]);
                int value = Parse(split[1]);
                
                if (rulesAsDict.ContainsKey(key))
                {
                    rulesAsDict[key].Add(value);
                }
                else
                {
                    rulesAsDict[key] = new List<int>(value);
                }
            }

            /*
             * Måske noget smart her med for loop over values i dict og lave dem alle til en bool og så
             * køre any()?
             */

            List<int[]> safeUpdates = new List<int[]>();
            List<int[]> unSafeUpdates = new List<int[]>();
            foreach (var update in updates)
            {
                bool isSafe = true;
                foreach (int number in update)
                {
                    //check if number exists in dict
                    if (rulesAsDict.ContainsKey(number))
                    {
                        //Check if index is fine
                        List<int> values = rulesAsDict[number];
                        foreach (int v in values)
                        {
                            int indexOfV = Array.IndexOf(update, v);
                            int indexOfNumber = Array.IndexOf(update, number);

                            // Check if `v` or `number` is not in the array
                            if (indexOfV == -1)
                            {
                                continue;
                            }
                            if (indexOfV < indexOfNumber)
                            {
                                isSafe = false;
                            }
                        }

                    }
                    
                }
                if (isSafe)
                {
                    safeUpdates.Add(update);
                }
                else
                {
                    unSafeUpdates.Add(update);
                }
                
            }
            
            int sum = 0;
            foreach (int[] v in safeUpdates)
            {
                sum += v[v.Length / 2];
            }
            
            Console.WriteLine($"Part 1 sum: {sum}");
            
        }
    }

}