using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1;

public class NumbersAsText
{
    public string Name { get; set; } = "";
    public int Value { get; set; }
}

public class Day1
{
    private readonly string _filePath = "D:\\Fede\\Projects\\AdventOfCode\\AdventOfCode\\Day1\\part1.txt";
    private readonly string _filePath2 = "D:\\Fede\\Projects\\AdventOfCode\\AdventOfCode\\Day1\\part2.txt";

    private readonly Dictionary<string, int> namedNumbers = new()
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };

    private readonly List<int> fileNumbers = new();

    public int GetTotalSumPart1()
    {
        string[] textLines = File.ReadAllLines(_filePath);
        int totalSum = 0;
        var sb = new StringBuilder();

        for (int i = 0; i < textLines.Length; i++)
        {
            foreach (char item in textLines[i])
            {
                if (int.TryParse(item.ToString(), out int _))
                {
                    sb.Append(item);
                    break;
                }
            }

            for (int j = textLines[i].Length - 1; j >= 0; j--)
            {
                var value = textLines[i][j];

                if (int.TryParse(value.ToString(), out int _))
                {
                    sb.Append(value);
                    break;
                }
            }
            fileNumbers.Add(int.Parse(sb.ToString()));
            sb.Clear();
        }

        foreach (var item in CollectionsMarshal.AsSpan(fileNumbers))
        {
            totalSum += item;
        }

        return totalSum;
    }

    public int GetTotalSum2Part2()
    {
        string[] textLines = File.ReadAllLines(_filePath2);
        int totalSum = 0;
        var sb = new StringBuilder();

        for (int i = 0; i < textLines.Length; i++)
        {
            var firstValue = GetFirstValue(textLines[i]);
            var lastValue = GetLastValue(textLines[i]);

            if (int.TryParse(firstValue, out int _))
            {
                sb.Append(firstValue);
            }
            else
            {
                sb.Append(namedNumbers[firstValue]);
            }

            if (int.TryParse(lastValue, out int _))
            {
                sb.Append(lastValue);
            }
            else
            {
                sb.Append(namedNumbers[lastValue]);
            }

            fileNumbers.Add(int.Parse(sb.ToString()));
            Console.WriteLine($"Number: {fileNumbers[i]} for text: {textLines[i]} on line {i}");
            sb.Clear();
        }

        foreach (var item in CollectionsMarshal.AsSpan(fileNumbers))
        {
            totalSum += item;
        }

        return totalSum;
    }

    private (string, int?) GetFirstNameNumber(string text)
    {
        string pattern = @"(" + string.Join("|", namedNumbers.Keys) + ")";
        MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

        if (matches.Count > 0)
        {
            return (matches[0].Value, matches[0].Index);
        }

        return ("", null);
    }

    private (string, int?) GetLastNameNumber(string text)
    {
        string pattern = @"(" + string.Join("|", namedNumbers.Keys) + ")";
        MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

        if (matches.Count > 0)
        {
            return (matches[^1].Value, matches[^1].Index);
        }

        return ("", null);
    }

    private (string, int?) GetFirstNumber(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            string item = text[i].ToString();
            if (int.TryParse(item, out int _))
            {
                return (item, i);
            }
        }

        return ("", null);
    }

    private (string, int?) GetLastNumber(string text)
    {
        for (int i = text.Length - 1; i >= 0; i--)
        {
            string item = text[i].ToString();
            if (int.TryParse(item, out int _))
            {
                return (item, i);
            }
        }

        return ("", null);
    }

    private string GetFirstValue(string text)
    {
        var firstNameNumber = GetFirstNameNumber(text);
        int? nameNumberIndex = firstNameNumber.Item2;

        var firstNumber = GetFirstNumber(text);
        int? numberIndex = firstNumber.Item2;

        if (nameNumberIndex is not null && numberIndex is null)
        {
            return firstNameNumber.Item1;
        }

        if (numberIndex is not null && nameNumberIndex is null)
        {
            return firstNumber.Item1;
        }

        if (nameNumberIndex < numberIndex)
        {
            return firstNameNumber.Item1;
        }

        return firstNumber.Item1;
    }

    private string GetLastValue(string text)
    {
        var lastNameNumber = GetLastNameNumber(text);
        int? nameNumberIndex = lastNameNumber.Item2;

        var lastNumber = GetLastNumber(text);
        int? numberIndex = lastNumber.Item2;

        if (nameNumberIndex is not null && numberIndex is null)
        {
            return lastNameNumber.Item1;
        }

        if (numberIndex is not null && nameNumberIndex is null)
        {
            return lastNumber.Item1;
        }

        if (nameNumberIndex > numberIndex)
        {
            return lastNameNumber.Item1;
        }

        return lastNumber.Item1;
    }
}