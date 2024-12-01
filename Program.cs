using AdventOfCode.Day1;

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
        }
    }
}