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
    public ConsolePrinter(GameMap map,List<SnakeObj> snakeObjs)
    {
        Map = map;  
        SnakeObjs = snakeObjs;
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

    public ConsoleColor WallColorForegroundColor = ConsoleColor.Red;
    public ConsoleColor WallColorBackgroundColor = ConsoleColor.White;

    public ConsoleColor Snake1Color = ConsoleColor.DarkGreen;
    public ConsoleColor Snake2Color = ConsoleColor.DarkBlue;

    public ConsoleColor EmptyCellBackgroundColor = ConsoleColor.Black;
    public ConsoleColor EmptyCellForegroundColor = ConsoleColor.Black;



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
        if (cell.SnakesValues is null)
        {
            var saveConsoleColor = new SaveConsoleColor();
            Console.BackgroundColor = EmptyCellBackgroundColor;
            Console.ForegroundColor = EmptyCellForegroundColor;

            Console.Write("e");

            LoadSavedConsoleState(saveConsoleColor);
        }

        else if (cell.SnakesValues.Count > 2)
            throw new NotImplementedException("for now the game only supports only 2 snakes");

        else
        if (cell.SnakesValues[0].snakeVlaue == 0)
        {
            var saveConsoleColor = new SaveConsoleColor();
            Console.BackgroundColor = EmptyCellBackgroundColor;
            Console.ForegroundColor = EmptyCellForegroundColor;

            Console.Write("e");

            LoadSavedConsoleState(saveConsoleColor);
            return;
        }

        else if (cell.SnakesValues[0].snakeVlaue > 0)
        {
            var saveConsoleColor = new SaveConsoleColor();

            Console.ForegroundColor = Snake1Color;
            Console.ForegroundColor = Snake1Color;

            Console.Write('1');

            LoadSavedConsoleState(saveConsoleColor);
            return;
        }
        else if (cell.SnakesValues.Count>1 &&
                 cell.SnakesValues[1].snakeVlaue > 1)
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

    public void Draw()
    {
        foreach (var row in Map.Map)
        {
            foreach (Cell cell in row) // this will iterate over all cells in row
            {
                if (cell.IsWall)
                    DrawWall();

                else
                {
                    DrawSnakes(cell);
                }



            }
            Console.WriteLine();
        }
    }
}
