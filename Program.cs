using AdventOfCode.Day1;
using AdventOfCode.Day2;

namespace Advent_of_code_2024;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter day to run");
        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1" :
                new Day1().Run();
                break;
            case "2":
                new Day2().Run();
                break;
            
            default:
                Console.WriteLine("No valid date selected");
                break;
        }
    }
}