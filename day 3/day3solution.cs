using System.Text.RegularExpressions;
using Advent_of_code_2024.Utilities;

namespace AdventOfCode.Day3
{
    public class Day3
    {
        public void Run()
        {

            var input = Utility.GetInput(3);

            var inputString = "";

            foreach (var x in input)
            {
                inputString = String.Concat(inputString, x);
            }
            
            int sum = new int();
            int partTwoSum = new int();

            Regex mulRegex = new Regex("mul\\(\\d+,\\d+\\)");
            Regex doRegex = new Regex("do\\(\\)");
            Regex dontRegex = new Regex("don't\\(\\)");

            List<int> doIndexes = doRegex.Matches(inputString).Select(match => match.Index).ToList();
            List<int> dontIndexes = dontRegex.Matches(inputString).Select(match => match.Index).ToList();
            
            foreach (Match m in mulRegex.Matches(inputString))
            {
                var stringOfMul = m.ToString();
                int endOfString = stringOfMul.LastIndexOf(")");
                String numCommaNum = stringOfMul.Substring(4, endOfString-4);
                var split = numCommaNum.Split(",");
                sum += Int32.Parse(split[0]) * Int32.Parse(split[1]);
            }
            
            Console.WriteLine($"Part 1 sum: {sum}");
            
            //starts with a do
            doIndexes.Insert(0, 1);
            
            foreach (Match match in mulRegex.Matches(inputString))
            {
                var matchIndex = match.Index;

                int closestInDo = doIndexes.TakeWhile(x => x < matchIndex).LastOrDefault();
                int closestInDont = dontIndexes.TakeWhile(x => x < matchIndex).LastOrDefault();
                
                if (matchIndex - closestInDo < matchIndex - closestInDont)
                {
                    var stringOfMul = match.ToString();
                    int endOfString = stringOfMul.LastIndexOf(")");
                    String numCommaNum = stringOfMul.Substring(4, endOfString-4);
                    var split = numCommaNum.Split(",");
                    partTwoSum += Int32.Parse(split[0]) * Int32.Parse(split[1]);
                }

            }
            
            Console.WriteLine($"Part 2 sum: {partTwoSum}");

        }
        
    }

}