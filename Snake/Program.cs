using Snake;

using System;
using System.Net.NetworkInformation;

public class SnakeGame
{
    public static void Main()
    {

        SnakeObj snake1 = new SnakeObj(5, 5, SnakeObj.Directions.right);

        List<SnakeObj> snakeObjs= new  List<SnakeObj> { snake1};

        var jsonStr = string.Join(' ', File.ReadAllLines(
            "C:\\Users\\sorou\\Source\\Repos\\Snake\\Snake\\GameMap.json"));

        var cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<Cell>>>(jsonStr)!;

        var gameMap = new GameMap(cells, snakeObjs);

        ConsoleManger consoleManger = new ConsoleManger(gameMap, snakeObjs);

        GameEngine gameEngine = new(consoleManger,gameMap, snakeObjs);
         gameEngine.MainGameLoop().Wait();

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