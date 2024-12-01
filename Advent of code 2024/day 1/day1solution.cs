using Advent_of_code_2024.Utilities;
using static System.Int32;

namespace AdventOfCode.Day1
{
    public class Day1
    {
        public void Run()
        {
            
            
            // Part 1:
            var input = Utility.GetInput(1);

            List<int> left = new List<int>();
            List<int> right = new List<int>();
                
            foreach (string line in input)
            {
                var split = line.Split("   ");
                left.Add(Int32.Parse(split[0]));
                right.Add(Int32.Parse(split[1]));
            }
            
            left.Sort();
            right.Sort();

            int sum = new int();
            for (int i = 0; i < left.Count; i++)
            {
                sum += Abs(left[i] - right[i]);
            }
            
            Console.WriteLine($"part 1 sum: {sum}");
            
            
            // Part 2:

            int part2Sum = new int();
            foreach (int number in left)
            {
                part2Sum += number * right.Where(s => s.Equals(number)).Count();
            }
            
            
            Console.WriteLine(part2Sum);
        }
    
    } 
}
