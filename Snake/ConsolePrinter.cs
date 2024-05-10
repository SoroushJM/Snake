using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

/// <summary>
/// can print mutiple snake into console
/// </summary>
public class ConsolePrinter
{
    GameMap Map;
    List<SnakeObj> SnakeObjs;
    StringBuilder DisplayMapStr;

    public ConsolePrinter(GameMap map, List<SnakeObj> snakeObjs)
    {
        Map = map;
        SnakeObjs = snakeObjs;
        int numberOfCharWeNeed = map._Map.Count * map._Map.First().Count * 13 * 2;
        var displayMap = new StringBuilder(numberOfCharWeNeed);
        DisplayMapStr = displayMap;
    }
    /// <summary>
    ///  On Creation it will saved foregroundColor and backgroundColor of console
    /// </summary>
   

    public string WallColorForegroundColor = "\u001b[38;5;231m";
    public string WallColorBackgroundColor = "\u001b[48;5;231m";

    public string Snake1Color = "\u001b[38;5;28m\u001b[48;5;28m";
    public string Snake2Color = "\u001b[38;5;21m";

    public string EmptyCellBackgroundColor = "\u001b[48;5;0m";
    public string EmptyCellForegroundColor = "\u001b[38;5;0m";

    public string AppelColorForeGround = "\u001b[48;5;125m";
    public string AppelColorBackGround = "\u001b[38;5;125m";





    public void DrawWall()
    {

        DisplayMapStr.Append(WallColorForegroundColor);
        DisplayMapStr.Append(WallColorBackgroundColor);
        DisplayMapStr.Append("W");

    }

    public void DrawSnakes(Cell cell)
    {

        if (cell.SnakesValues.Count > 2)
            throw new NotImplementedException("for now the game only supports only 2 snakes");

        else
        if (cell.SnakesValues[0]._SnakeValue == 0)
        {
            
            DisplayMapStr.Append(EmptyCellBackgroundColor);
            DisplayMapStr.Append(EmptyCellForegroundColor);

            DisplayMapStr.Append("e");

            return;
        }

        else if (cell.SnakesValues[0]._SnakeValue > 0)
        {

            DisplayMapStr.Append(Snake1Color);
            DisplayMapStr.Append(Snake1Color);

            DisplayMapStr.Append('1');

            return;
        }
        else if (cell.SnakesValues.Count > 1 &&
                 cell.SnakesValues[1]._SnakeValue > 1)
        {

            DisplayMapStr.Append(Snake2Color);
            DisplayMapStr.Append(Snake2Color);

            DisplayMapStr.Append('2');

            return;
        }

        throw new Exception("this function should never reach this state, this means you have error somewhere");

    }

    public void DrawAppel()
    {

        DisplayMapStr.Append(AppelColorBackGround);
        DisplayMapStr.Append(AppelColorForeGround);
        DisplayMapStr.Append("A");

    }

    public bool IsCellEmpty(Cell cell)
    {
        if (cell.SnakesValues.Any(x => x._SnakeValue != 0) && cell.IsWall == false) 
        {
            return false;
        }
        return true;
    }


    public void DrawEmptyCell(Cell cell)
    {
        DisplayMapStr.Append(EmptyCellBackgroundColor);
        DisplayMapStr.Append(EmptyCellForegroundColor);
        DisplayMapStr.Append("e");
    }
    public void Draw()
    {
        DisplayMapStr.Clear();
        Console.Clear();
        foreach (var row in Map._Map)
        {
            foreach (Cell cell in row) // this will iterate over all cells in row
            {
                if (cell.IsWall)
                {
                    DrawWall();
                }
                else if (cell.HaveAppel)
                {
                    DrawAppel();
                }
                else if (cell.SnakesValues.Count > 0)
                {
                    DrawSnakes(cell);
                }
                else if (IsCellEmpty(cell))
                {
                    DrawEmptyCell(cell);
                }
                else
                {

                    throw new Exception("this code path is not valid It should never reach here");
                }

            }
            DisplayMapStr.Append("\n\u001b[38;5;0m\u001b[48;5;0m");
        }
        Console.WriteLine(DisplayMapStr);
    }
}
