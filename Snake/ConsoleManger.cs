using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class ConsoleManger : ConsolePrinter
{
    public event EventHandler? On_KeyPress_W;
    public event EventHandler? On_KeyPress_S;
    public event EventHandler? On_KeyPress_A;
    public event EventHandler? On_KeyPress_D;

    public ConsoleManger(GameMap map, List<SnakeObj> snakeObjs) : base(map, snakeObjs)
    {
    }

    public void HandelInput()
    {

        while (true)
        {
            var keyPressed = Console.ReadKey(true).Key;
            switch (keyPressed)
            {
                case ConsoleKey.W:

                    On_KeyPress_W?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.S:
                    On_KeyPress_S?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.A:
                    On_KeyPress_A?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.D:
                    On_KeyPress_D?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }
    }

}
