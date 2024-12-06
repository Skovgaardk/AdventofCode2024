using Advent_of_code_2024.Utilities;

namespace AdventOfCode.Day6;

public class Day6
{
    public void Run()
    {
        var input = Utility.GetInput(6);

        char[] guard = { '<', '>', '^', 'v' };
        
        //get guard and x y coordinates
        var result = input
            .SelectMany((s, x) => s
                .Select((c, y) => new { Char = c, X = x, Y = y }))
            .FirstOrDefault(item => guard.Contains(item.Char));

        var inputAsChars = input
            .Select(x => x.ToCharArray()).ToArray();

        var sum = mapMoveFunct(result.Char, result.X, result.Y, inputAsChars, 1);
        Console.WriteLine($"Part 1 sum: {sum}");
        
    }

    private int mapMoveFunct(char c, int x, int y, char[][] input, int sum)
    {
        var rows = input.Length;
        var cols = input[0].Length;
        
        var directions = new Dictionary<char, (int dx, int dy, char next)>
        {
            { '^', (-1, 0, '>') },
            { '>', (0, 1, 'v') },
            { 'v', (1, 0, '<') },
            { '<', (0, -1, '^') }
        };


        while (true)
        {
            // Check if we've hit an end
            if ((c == '^' && y == 0) ||
                (c == 'v' && y == rows - 1) ||
                (c == '>' && x == cols - 1) ||
                (c == '<' && x == 0))
            {
                return sum;
            }


            if (directions.TryGetValue(c, out var dir))
            {
                int nx = x + dir.dx, ny = y + dir.dy;

                if (nx < 0 || nx >= rows || ny < 0 || ny >= cols)
                    return sum;

                var nextChar = input[nx][ny];
                
                if (nextChar == '.')
                {
                    input[x][y] = c;
                    x = nx;
                    y = ny;
                    input[x][y] = c;
                    sum++;
                }
                //check if next char is already a direction
                else if (directions.ContainsKey(nextChar))
                {
                    input[x][y] = c;
                    x = nx;
                    y = ny;
                    input[x][y] = c;
                }
                else if (nextChar == '#')
                {
                    c = dir.next;

                    // Try to move in the new direction
                    var turnedNx = x + directions[c].dx;
                    var turnedNy = y + directions[c].dy;

                    // Check if the turned direction is valid
                    if (turnedNx >= 0 && turnedNx < rows &&
                        turnedNy >= 0 && turnedNy < cols &&
                        input[turnedNx][turnedNy] == '.')
                    {
                        input[x][y] = c;
                        x = turnedNx;
                        y = turnedNy;
                        input[x][y] = c;
                        sum++;
                    }
                    // If it is not valid it will end up here again and turn again, this should be made a function/loop for optimization but im lazy
                    
                }
            }

        }
    }
}