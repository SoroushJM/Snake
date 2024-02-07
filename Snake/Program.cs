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

        List<SnakeObj> SnakeObjs= new  List<SnakeObj> { snake1};

        GameMap = new GameMap(SnakeObjs,new MapSize());




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