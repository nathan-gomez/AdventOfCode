using System.Drawing;

namespace AdventOfCode.Day2;

public class Day2
{
    private sealed class Cube
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

    private readonly Cube validRedGame = new Cube
    {
        Quantity = 12,
        Color = "RED"
    };

    private readonly Cube validBlueGame = new Cube
    {
        Quantity = 14,
        Color = "BLUE"
    };

    private readonly Cube validGreenGame = new Cube
    {
        Quantity = 13,
        Color = "GREEN"
    };

    private readonly string _filePath = "D:\\Fede\\Projects\\AdventOfCode\\AdventOfCode\\Day2\\data.txt";

    public int GetPossibleGames()
    {
        string[] textLines = File.ReadAllLines(_filePath);
        int sum = 0;
        foreach (string line in textLines)
        {
            int gameSetId = GetGameId(line);
            string[] cubeSets = line.Split("; ");
            cubeSets[0] = cubeSets[0].Replace($"Game {gameSetId}: ", "");

            if (IsValidGame(cubeSets))
            {
                sum += gameSetId;
            }
        }

        return sum;
    }

    public int GetMinimumCubesSets()
    {
        string[] textLines = File.ReadAllLines(_filePath);
        int sum = 0;
        foreach (string line in textLines)
        {
            int gameSetId = GetGameId(line);
            string[] cubeSets = line.Split("; ");
            cubeSets[0] = cubeSets[0].Replace($"Game {gameSetId}: ", "");

            var minimunCubesUsed = GetMinimumCubes(cubeSets);

            sum += minimunCubesUsed;
        }

        return sum;
    }

    private int GetMinimumCubes(string[] cubeSets)
    {
        var maxGreenCube = 0;
        var maxBlueCube = 0;
        var maxRedCube = 0;

        foreach (string item in cubeSets)
        {
            var cubeSet = ExtractCubeData(item);

            for (int i = 0; i < cubeSet.Count; i++)
            {
                if (cubeSet[i].Color == "GREEN")
                {
                    var maxVal = Math.Max(cubeSet[i].Quantity, maxGreenCube);
                    maxGreenCube = maxVal;
                }

                if (cubeSet[i].Color == "RED")
                {
                    var maxVal = Math.Max(cubeSet[i].Quantity, maxRedCube);
                    maxRedCube = maxVal;
                }

                if (cubeSet[i].Color == "BLUE")
                {
                    var maxVal = Math.Max(cubeSet[i].Quantity, maxBlueCube);
                    maxBlueCube = maxVal;
                }
            }
        }

        var cubeSetPower = maxRedCube * maxGreenCube * maxBlueCube;

        return cubeSetPower;
    }

    private int GetGameId(string line)
    {
        var idx1 = line.IndexOf(' ');
        var idx2 = line.IndexOf(':');
        int gameSetId = int.Parse(line[idx1..idx2].ToString());

        return gameSetId;
    }

    private bool IsValidGame(string[] cubeSetString)
    {
        foreach (var item in cubeSetString)
        {
            var cubeSet = ExtractCubeData(item);

            foreach (var cube in cubeSet)
            {
                switch (cube.Color)
                {
                    case "RED":
                        {
                            if (cube.Quantity > validRedGame.Quantity)
                            {
                                return false;
                            }
                            break;
                        }
                    case "GREEN":
                        {
                            if (cube.Quantity > validGreenGame.Quantity)
                            {
                                return false;
                            }
                            break;
                        }
                    case "BLUE":
                        {
                            if (cube.Quantity > validBlueGame.Quantity)
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
        }

        return true;
    }

    private List<Cube> ExtractCubeData(string data)
    {
        var cubeList = new List<Cube>();
        var cubes = data.Split(", ");
        foreach (var cube in cubes)
        {
            var cubeArr = cube.Split(' ');
            cubeList.Add(new Cube
            {
                Quantity = int.Parse(cubeArr[0]),
                Color = cubeArr[1].ToUpper()
            });
        }

        return cubeList;
    }
}