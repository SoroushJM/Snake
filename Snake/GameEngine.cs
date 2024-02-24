using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class GameEngine
{
    GameMap _GameMap;
    List<List<Cell>> _Map;
    Task _ConsoleHandller;
    List<SnakeObj> _SnakeObjs {  get; set; }
    ConsoleManger _consoleManger;

    public GameEngine(ConsoleManger consoleManger,GameMap gameMap,List<SnakeObj> snakeObjs)
    {
        _consoleManger = consoleManger;

        consoleManger.On_KeyPress_A += A_Key_Pressed;
        consoleManger.On_KeyPress_D += D_Key_Pressed;
        consoleManger.On_KeyPress_W += W_Key_Pressed;
        consoleManger.On_KeyPress_S += S_Key_Pressed;

        _SnakeObjs = snakeObjs;
        _GameMap = gameMap;
        _Map = gameMap._Map;
        Task? consoleHandller =Task.Run(consoleManger.HandelInput);
        _ConsoleHandller = consoleHandller;
    }
    public static void W_Key_Pressed(object? sender, EventArgs e)
    {

    }
    public static void S_Key_Pressed(object? sender, EventArgs e)
    {

    }
    public static void A_Key_Pressed(object? sender, EventArgs e)
    {

    }
    public static void D_Key_Pressed(object? sender, EventArgs e)
    {

    }

    public async Task MainGameLoop()
    {
        _ConsoleHandller = Task.Run(() => _consoleManger.HandelInput());
        while (true)
        {

            UpdateSnakes();

            _consoleManger.Draw();
            await Task.Delay(3000);
        }
    }

    public void MoveUpSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;

        var currentHeadValue = _Map[x][y].SnakesValues.First(x => x._SnakeObj == snakeObj)._SnakeValue;
        _Map[x][y-1].SnakesValues.First(x => x._SnakeObj == snakeObj)._SnakeValue = currentHeadValue+1;


    }

    public void UpdateSnakes()
    {
        foreach (var snakeObj in _SnakeObjs)
        {
            switch (snakeObj.Direction)
            {
                case SnakeObj.Directions.up:
                    MoveUpSnake(snakeObj);
                    break;
                case SnakeObj.Directions.down:
                    break;
                case SnakeObj.Directions.left:
                    break;
                case SnakeObj.Directions.right:
                    break;
                default:
                    break;
            }
        }
    }



}
