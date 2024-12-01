namespace Advent_of_code_2024.Utilities;

public static class Utility
{
    
    public static string[] GetInput(int day)
    {

        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            
            var path = Path.Combine(basePath, $"day {day}", "input.txt");
            
            return File.ReadAllLines(path);
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read");
            Console.WriteLine(e);
            throw;
        }
    }
}