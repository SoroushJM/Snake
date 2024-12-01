using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

/// <summary>
/// can print multiple snake into console
/// </summary>
public class ConsolePrinter
{
    GameMap Map;
    List<SnakeObj> SnakeObjs;
    List<string> FinalStr2Print = new();
    const string NewLine_BlackBackGround_BlackForegrand = "\n\u001b[38;5;0m\u001b[48;5;0m";

    public ConsolePrinter(GameMap map, List<SnakeObj> snakeObjs)
    {
        Map = map;
        SnakeObjs = snakeObjs;

    }

    public const string WallColorForegroundColor = "\u001b[38;5;231m";
    public const string WallColorBackgroundColor = "\u001b[48;5;231m";

    public const string Snake1Color = "\u001b[38;5;28m\u001b[48;5;28m";
    public const string Snake2Color = "\u001b[38;5;21m";

    public const string EmptyCellBackgroundColor = "\u001b[48;5;0m";
    public const string EmptyCellForegroundColor = "\u001b[38;5;0m";

    public const string AppelColorForeGround = "\u001b[48;5;125m";
    public const string AppelColorBackGround = "\u001b[38;5;125m";





    public void DrawWall()
    {

        FinalStr2Print.Add(WallColorForegroundColor);
        FinalStr2Print.Add(WallColorBackgroundColor);
        FinalStr2Print.Add("W");
        FinalStr2Print.Add("W");


    }

    public void DrawSnakes(Cell cell)
    {

        if (cell.SnakesValues.Count > 2)
            throw new NotImplementedException("for now the game only supports only 2 snakes");

        else
        if (cell.SnakesValues[0]._SnakeValue == 0)
        {

            FinalStr2Print.Add(EmptyCellBackgroundColor);
            FinalStr2Print.Add(EmptyCellForegroundColor);

            FinalStr2Print.Add("e");
            FinalStr2Print.Add("e");


            return;
        }

        else if (cell.SnakesValues[0]._SnakeValue > 0)
        {

            FinalStr2Print.Add(Snake1Color);
            FinalStr2Print.Add(Snake1Color);

            FinalStr2Print.Add("1");
            FinalStr2Print.Add("1");

            return;
        }
        else if (cell.SnakesValues.Count > 1 &&
                 cell.SnakesValues[1]._SnakeValue > 1)
        {

            FinalStr2Print.Add(Snake2Color);
            FinalStr2Print.Add(Snake2Color);

            FinalStr2Print.Add("2");
            FinalStr2Print.Add("2");

            return;
        }

        throw new Exception("this function should never reach this state, this means you have error somewhere");

    }

    public void DrawAppel()
    {

        FinalStr2Print.Add(AppelColorBackGround);
        FinalStr2Print.Add(AppelColorForeGround);
        FinalStr2Print.Add("A");
        FinalStr2Print.Add("A");


    }

    public void DrawEmptyCell(Cell cell)
    {
        FinalStr2Print.Add(EmptyCellBackgroundColor);
        FinalStr2Print.Add(EmptyCellForegroundColor);
        FinalStr2Print.Add("e");
        FinalStr2Print.Add("e");

    }


    public bool IsCellEmpty(Cell cell)
    {
        if (cell.SnakesValues.Any(x => x._SnakeValue != 0) && cell.IsWall == false)
        {
            return false;
        }
        return true;
    }
    public void DrawGameLostScreen()
    {

    }

    public void Draw()
    {
        //int lastLoopItemCount = FinalStr2Print.Capacity;
        FinalStr2Print.Clear();


        //if(lastLoopItemCount > 0) 
        //    FinalStr2Print.EnsureCapacity(lastLoopItemCount);

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
            FinalStr2Print.Add(NewLine_BlackBackGround_BlackForegrand);
        }
        FinalStr2Print.Add(NewLine_BlackBackGround_BlackForegrand);
        Console.WriteLine(string.Join(string.Empty, FinalStr2Print));
    }
}
