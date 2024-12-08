using System.Security.Cryptography;
using Advent_of_code_2024.Utilities;


namespace AdventOfCode.Day7;

public class Day7
{

    static void GeneratePermutations(int length, string current, List<string> permutations)
    {
        if (current.Length == length)
        {
            permutations.Add(current);
            return;
        }
        
        GeneratePermutations(length, current + "+", permutations);
        GeneratePermutations(length, current + "*", permutations);
    }
    public void Run()
    {
        var input = Utility.GetInput(7);
        
        List<ulong> answers = new List<ulong>();
        List<List<ulong>> numbers = new List<List<ulong>>();

        foreach (string s in input)
        {
            var split = s.Split(" ");
            // append the first number minus its last character to the answers array
            answers.Add(ulong.Parse(split[0][..^1]));
            
            // append the rest of the numbers to the numbers array
            numbers.Add(split[1..].Select(ulong.Parse).ToList());
        }
        
        ulong sum = 0;
        
        // iterate over the answers array
        for (int i = 0; i < answers.Count; i++)
        {
            int numOfOperators = numbers[i].Count - 1;
            List<string> permutations = new List<string>();
            GeneratePermutations(numOfOperators, "", permutations);
            
            // now we can iterate over the permutations array, and calculate the result of the expression
            // If the result is equal to the answer, we increment the sum
            foreach (string permutation in permutations)
            {
                ulong result = numbers[i][0];
                for (int j = 0; j < permutation.Length; j++)
                {
                    if (permutation[j] == '+')
                    {
                        result += numbers[i][j + 1];
                    }
                    else
                    {
                        result *= numbers[i][j + 1];
                    }
                    
                }
        
                if (result == answers[i])
                {
                    sum += result;
                    break;
                }
            }
            
        }
    
        Console.WriteLine($"Part 1 sum: {sum}");

    }
}