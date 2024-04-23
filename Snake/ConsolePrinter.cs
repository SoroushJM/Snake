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
    public ConsolePrinter(GameMap map, List<SnakeObj> snakeObjs)
    {
        Map = map;
        SnakeObjs = snakeObjs;
        int numberOfCharWeNeed = map._Map.Count * map._Map.First().Count * 13;
        var displayMap = new StringBuilder(numberOfCharWeNeed);
    }
    /// <summary>
    ///  On Creation it will saved foregroundColor and backgroundColor of console
    /// </summary>
    public struct SaveConsoleColor
    {
        public SaveConsoleColor()
        {
            foregroundColor = Console.ForegroundColor;
            backgroundColor = Console.BackgroundColor;
        }
        public ConsoleColor foregroundColor;
        public ConsoleColor backgroundColor;
    }

    public ConsoleColor WallColorForegroundColor = ConsoleColor.White;
    public ConsoleColor WallColorBackgroundColor = ConsoleColor.White;

    public ConsoleColor Snake1Color = ConsoleColor.DarkGreen;
    public ConsoleColor Snake2Color = ConsoleColor.DarkBlue;

    public ConsoleColor EmptyCellBackgroundColor = ConsoleColor.Black;
    public ConsoleColor EmptyCellForegroundColor = ConsoleColor.Black;

    public ConsoleColor AppelColorForeGround = ConsoleColor.Red;
    public ConsoleColor AppelColorBackGround = ConsoleColor.Red;




    public void LoadSavedConsoleState(SaveConsoleColor savedConsoleColor)
    {
        Console.BackgroundColor = savedConsoleColor.backgroundColor;
        Console.ForegroundColor = savedConsoleColor.foregroundColor;
    }




    public void DrawWall()
    {

        var saveConsoleStated = new SaveConsoleColor();

        Console.ForegroundColor = WallColorForegroundColor;
        Console.BackgroundColor = WallColorBackgroundColor;
        Console.Write("W");

        LoadSavedConsoleState(saveConsoleStated);
    }

    public void DrawSnakes(Cell cell)
    {

        if (cell.SnakesValues.Count > 2)
            throw new NotImplementedException("for now the game only supports only 2 snakes");

        else
        if (cell.SnakesValues[0]._SnakeValue == 0)
        {
            var saveConsoleColor = new SaveConsoleColor();
            Console.BackgroundColor = EmptyCellBackgroundColor;
            Console.ForegroundColor = EmptyCellForegroundColor;

            Console.Write("e");

            LoadSavedConsoleState(saveConsoleColor);
            return;
        }

        else if (cell.SnakesValues[0]._SnakeValue > 0)
        {
            var saveConsoleColor = new SaveConsoleColor();

            Console.ForegroundColor = Snake1Color;
            Console.BackgroundColor = Snake1Color;

            Console.Write('1');

            LoadSavedConsoleState(saveConsoleColor);
            return;
        }
        else if (cell.SnakesValues.Count > 1 &&
                 cell.SnakesValues[1]._SnakeValue > 1)
        {
            var saveConsoleColor = new SaveConsoleColor();

            Console.ForegroundColor = Snake2Color;
            Console.ForegroundColor = Snake2Color;

            Console.Write('2');

            LoadSavedConsoleState(saveConsoleColor);
            return;
        }

        throw new Exception("this function should never reach this state, this means you have error somewhere");

    }

    public void DrawAppel()
    {
        SaveConsoleColor saveConsoleColor = new SaveConsoleColor();

        Console.BackgroundColor = AppelColorBackGround;
        Console.ForegroundColor = AppelColorForeGround;
        Console.Write("A");
        LoadSavedConsoleState(saveConsoleColor);

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
        //var saveConsoleState = new SaveConsoleColor();
        Console.Write("e");
        //LoadSavedConsoleState(saveConsoleState);
    }
    public void Draw()
    {
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
            Console.WriteLine();
        }
    }
}
