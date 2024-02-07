using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Snake;


/// <summary>
/// a class that handels Map of game
/// a 2d arrays of cells
/// </summary>
public class GameMap
{
    private int NumberOfSnakes { get; } // total number of snakes
    private List <SnakeObj> SnakeObjs { get; }

    List<List<Cell>> Map { get; }

    MapSize MapSize { get; }



    public GameMap(List<SnakeObj> snakeObjs, MapSize mapSize)
    {
        NumberOfSnakes = snakeObjs.Count;
        SnakeObjs = snakeObjs;
        MapSize = mapSize;
        Map = new List<List<Cell>> { };

        for (int i = 0; i < mapSize.coulmn; i++)
        {
            Map.Add(new List<Cell>());
        }

        for (int i = 0; i < mapSize.coulmn; i++)
        {
            for (int j = 0; j < mapSize.row; j++)
            {
                var cell = new Cell(snakeObjs);
                Map[i].Add(cell);
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rank">demestion rank-starts with 1</param>
    /// <returns>number of lenght in specifed ransk</returns>
 
    public int GetLength(int rank)
    {
        if (rank == 1)
        {
            return MapSize.coulmn;
        }
        if (rank ==2)
        {
            return MapSize.row;
        }
        throw new Exception($"this {rank} you provided is not valid");
    }


}
/// <summary>
/// A list of Snakes that will exist in this cell
/// with thier value
/// </summary>
internal class Cell
{
    bool IsWall = false;
    private int NumberOfSnakes { get; } // total number of snakes
    List<SnakeValues> SnakesValues ;
    public Cell(List<SnakeObj> snakeObjs)
    {
        NumberOfSnakes = snakeObjs.Count;
        SnakesValues = new() { };
        foreach (var snake in snakeObjs)
        {
            SnakesValues.Add(new SnakeValues { snakeObj = snake, snakeVlaue = 0 });
        }
    }

}

/// <summary>
/// snake and its value
/// </summary>
internal struct SnakeValues
{

    public SnakeObj snakeObj;
    public int snakeVlaue;
}