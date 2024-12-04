using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Advent_of_code_2024.Utilities;

namespace AdventOfCode.Day4
{
    public class Day4
    {

        private int getXMASes(char[] arr)
        {
            int count = 0;
            for (int i = 0; i <= arr.Length - 4; i++)  // Check if word can fit
            {
                if (new string(arr, i, 4) == "XMAS")
                {
                    count++;
                }
            }
            return count;
        }

        private int getXMASPattern(char[,] grid)
        {
            int count = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    bool isXMASForward = grid[i - 1, j - 1] == 'M' && grid[i, j] == 'A' && grid[i + 1, j + 1] == 'S' &&
                                         grid[i - 1, j + 1] == 'M'  && grid[i + 1, j - 1] == 'S';
                    bool isXMASBackward = grid[i - 1, j + 1] == 'S' && grid[i, j] == 'A' && grid[i + 1, j - 1] == 'M' &&
                                          grid[i - 1, j - 1] == 'S'  && grid[i + 1, j + 1] == 'M';


                    if (isXMASForward || isXMASBackward)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        public void Run()
        {

            var input = Utility.GetInput(4);
            
            int forwardXmas = 0;
            int backWardsXmas = 0;
            List<char[]> charArrayList = new List<char[]>();
            
            foreach (string s in input)
            {
                char[] chars = s.ToCharArray();
                charArrayList.Add(chars);
                forwardXmas += getXMASes(chars);
                Array.Reverse(chars);
                backWardsXmas += getXMASes(chars);
            
            }
            
            List<char[]> verticals = new List<char[]>();
            
            
            for (int i = 0; i < charArrayList[0].Length; i++)
            {
                string s = "";
                for (int j = 0; j < charArrayList.Count; j++)
                {
                    s = String.Concat(s, charArrayList[j][i]);
                }
            
                char[] charToList = s.ToCharArray();
                verticals.Add(charToList);
            }
            
            int verticalXMAS = 0;
            int reverseVerticalXMAS = 0;
            foreach (char[] c in verticals)
            {
                verticalXMAS += getXMASes(c);
                Array.Reverse(c);
                reverseVerticalXMAS += getXMASes(c);
            }
            
            var diagonals = input.SelectMany((row, rowIdx) => 
                    row.Select((x, colIdx) => new { Key = rowIdx - colIdx, Value = x }))
                .GroupBy(x => x.Key)
                .OrderBy(x => x.Key)
                .Select(values => values.Select(i => i.Value).ToArray())
                .ToArray();
            
            var reverseDiagonals = input.SelectMany((row, rowIdx) => 
                    row.Select((x, colIdx) => new { Key = rowIdx + colIdx, Value = x }))
                .GroupBy(x => x.Key)
                .OrderBy(x => x.Key)
                .Select(values => values.Select(i => i.Value).ToArray())
                .Where(d => d.Length >= 4) // Only consider diagonals long enough to contain "XMAS"
                .ToArray();
            
            int diagonalXMAS = 0;
            int reverseDiagonalXMAS = 0;
            foreach (char[] c in diagonals)
            {
                diagonalXMAS += getXMASes(c);
                Array.Reverse(c);
                reverseDiagonalXMAS += getXMASes(c);
            }
            foreach (char[] c in reverseDiagonals)
            {
                diagonalXMAS += getXMASes(c);
                Array.Reverse(c);
                reverseDiagonalXMAS += getXMASes(c);
            }

            int sum = forwardXmas + backWardsXmas + verticalXMAS + reverseVerticalXMAS + diagonalXMAS +
                      reverseDiagonalXMAS;
            
            Console.WriteLine($"part 1 sum: {sum}");
            
            int rows = input.Length;
            int cols = input[0].Length;
            
            char[,] grid = new char[rows,cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }
            
            // Does not return the correct answer
            int part2Sum = getXMASPattern(grid);
            Console.WriteLine($"Part 2 sum: {part2Sum}");

        }
    }
    

}