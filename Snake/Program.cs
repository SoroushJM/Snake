using Snake;

using System;
using System.Net.NetworkInformation;

public class SnakeGame
{
    public static GameMap GameMap;
    const int NumberOfSnakes = 1;
    public static void Main()
    {

        SnakeObj snake1 = new SnakeObj(5,5,SnakeObj.Directions.right);

        List<SnakeObj> snakeObjs= new  List<SnakeObj> { snake1};

        var jsonStr = string.Join(' ', File.ReadAllLines("C:\\Users\\Paratech\\source\\repos\\Snake\\Snake\\10X10Map.json"));

        var cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<Cell>>>(jsonStr)!;

        var gameMap = new GameMap(cells, snakeObjs);
        GameMap = gameMap;

        ConsolePrinter consolePrinter = new ConsolePrinter(gameMap, snakeObjs);

        




        while (Console.ReadKey().Key != ConsoleKey.Enter) {
            Console.Write("\b \b");


        }
    }


    
}
public struct MapSize
{
    public int coulmn = 10;
    public int row = 10;

    public MapSize()
    {
    }
}