using Advent_of_code_2024.Utilities;

namespace AdventOfCode.Day8;

public class Day8
{   
    private int CheckPlacement(char[][] grid, int x, int y, char antennaKey)
    {
        if (x >= 0 && y >= 0 && x < grid.Length && y < grid[0].Length)
        {
            bool posIsAntenna = grid[x][y] == antennaKey;
            bool posIsOccupied = grid[x][y] != '.';
            if (!posIsAntenna && !posIsOccupied)
            {
                return 1;
            }
        }
        return 0;
    }
    public void Run()
    {
        var input = Utility.GetInput(8);
        
        var inputAsChars = input
            .Select(x => x.ToCharArray()).ToArray();

        // Dictionary mapping antennas to their coordinates
        Dictionary<char, List<(int x, int y)>> coordinates = new();
        
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {

                if (input[i][j] == '.') continue;
                if (!coordinates.ContainsKey(input[i][j]))
                {
                    (int x, int y) coords = (i, j);
                    coordinates.Add(input[i][j], new List<(int, int)>());
                    coordinates[input[i][j]].Add(coords);
                }
                else
                {
                    (int x, int y) coords = (i, j);
                    coordinates[input[i][j]].Add(coords);
                }
            }
        }

        // now we need to iterate over all the combinations of antenna coordinates
        // For every combination of coordinates, we calculate the distance (vector) between the two antennas
        // we then need to check if the vector fits inside the grid, and if it does, that the positions are not occupied by the same frequency
        // if all conditions are met we can place a new frequency in the grid
        
        int sum = 0;
        //iterate over all the antennas
                foreach (var antenna in coordinates)
        {
            var positions = antenna.Value;

            // Iterate over unique pairs of positions
            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    var (x1, y1) = positions[i];
                    var (x2, y2) = positions[j];
                    int dx = x2 - x1;
                    int dy = y2 - y1;

                    // Check all 4 potential placements
                    sum += CheckPlacement(inputAsChars, x1 + dx, y1 + dy, antenna.Key);
                    sum += CheckPlacement(inputAsChars, x2 - dx, y2 - dy, antenna.Key);
                    sum += CheckPlacement(inputAsChars, x1 - dx, y1 - dy, antenna.Key);
                    sum += CheckPlacement(inputAsChars, x2 + dx, y2 + dy, antenna.Key);
                }
            }
        }

        
        
        Console.WriteLine($"Part 1 sum: {sum} (not correct)");

    }
}