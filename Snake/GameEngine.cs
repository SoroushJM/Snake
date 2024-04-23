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
    List<SnakeObj> _SnakeObjs { get; set; }
    ConsoleManger _consoleManger;

    public int NumberOfAppelsInMap { get; set; }

    public GameEngine(ConsoleManger consoleManger, GameMap gameMap, List<SnakeObj> snakeObjs)
    {
        _consoleManger = consoleManger;

        consoleManger.On_KeyPress_A += A_Key_Pressed;
        consoleManger.On_KeyPress_D += D_Key_Pressed;
        consoleManger.On_KeyPress_W += W_Key_Pressed;
        consoleManger.On_KeyPress_S += S_Key_Pressed;

        _SnakeObjs = snakeObjs;
        _GameMap = gameMap;
        _Map = gameMap._Map;
        Task? consoleHandller = Task.Run(consoleManger.HandelInput);
        _ConsoleHandller = consoleHandller;
    }

    public async Task MainGameLoop()
    {
        _ConsoleHandller = Task.Run(() => _consoleManger.HandelInput());
        while (true)
        {
            UpdateSnakesWhoEatAppel();

            AddAppelIfDoesntExist();

            _consoleManger.Draw();

            UpdateSnakes();

            await Task.Delay(1000/4);
        }
    }

    public void W_Key_Pressed(object? sender, EventArgs e)
    {
        _SnakeObjs[0].Direction = SnakeObj.Directions.up;
    }
    public void S_Key_Pressed(object? sender, EventArgs e)
    {
        _SnakeObjs[0].Direction = SnakeObj.Directions.down;
    }
    public void A_Key_Pressed(object? sender, EventArgs e)
    {
        _SnakeObjs[0].Direction = SnakeObj.Directions.left;
    }
    public void D_Key_Pressed(object? sender, EventArgs e)
    {
        _SnakeObjs[0].Direction = SnakeObj.Directions.right;
    }


    public void MoveUpSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;

        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeValue;
        _Map[x - 1][y].GetSnakeValues(snakeObj)!._SnakeValue = currentHeadValue + 1;


        snakeObj.HeadX = x - 1;
        snakeObj.HeadY = y;

    }
    public void MoveDownSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;

        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeValue;
        _Map[x + 1][y].GetSnakeValues(snakeObj)!._SnakeValue = currentHeadValue + 1;


        snakeObj.HeadX = x + 1;
        snakeObj.HeadY = y;
    }
    public void MoveRightSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;
        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeValue;
        _Map[x][y + 1].GetSnakeValues(snakeObj)!._SnakeValue = currentHeadValue + 1;


        snakeObj.HeadX = x;
        snakeObj.HeadY = y + 1;
    }
    public void MoveLeftSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;
        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeValue;
        _Map[x][y - 1].GetSnakeValues(snakeObj)!._SnakeValue = currentHeadValue + 1;


        snakeObj.HeadX = x;
        snakeObj.HeadY = y - 1;
    }


    public void MoveSnakes()
    {
        foreach (SnakeObj snakeObj in _SnakeObjs)
        {
            switch (snakeObj.Direction)
            {
                case SnakeObj.Directions.up:
                    MoveUpSnake(snakeObj);
                    break;


                case SnakeObj.Directions.down:
                    MoveDownSnake(snakeObj);
                    break;


                case SnakeObj.Directions.left:
                    MoveLeftSnake(snakeObj);
                    break;


                case SnakeObj.Directions.right:
                    MoveRightSnake(snakeObj);
                    break;


                default:
                    break;
            }
        }
    }

    public void RemoveOldValueFromMap()
    {
        foreach (var row in _Map)
        {
            foreach (var cell in row)
            {
                foreach (var snakeValues in cell.SnakesValues)
                {
                    if (snakeValues._SnakeValue > 0)
                        snakeValues._SnakeValue = snakeValues._SnakeValue - 1;
                }
            }
        }
    }

    public void AddAppelIfDoesntExist()
    {
        if (_GameMap.AppelCount == 0)
        {
            int x; int y;
            do
            {
                Random random = new Random();
                x = random.Next(_Map.Count);
                y = random.Next(_Map[0].Count);

            }
            while (!IsValidPlace4Appel(x,y));

            _Map[x][y].HaveAppel = true;

            _GameMap.AppelCount++;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>returns true if snake exist in that cell</returns>
    public bool IsValidPlace4Appel(int x, int y)
    {
        foreach (var snakeVal in _Map[x][y].SnakesValues)
        {
            if (snakeVal._SnakeValue > 0)
            {
                return false;
            }
            if (_Map[x][y].IsWall)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateSnakesWhoEatAppel()
    {
        foreach (var snake in _SnakeObjs)
        {
            int headX = snake.HeadX;
            int headY = snake.HeadY;

            if (_Map[headX][headY].HaveAppel == true)
            {
                _Map[headX][headY].HaveAppel = false;
                _GameMap.AppelCount--;

                var cell = _Map[headX][headY];

                SnakeValues x = cell.SnakesValues.Find(x => x._SnakeObj == snake)!;

                x._SnakeValue++;

            }

        }
    }

    public void UpdateSnakes()
    {
        MoveSnakes();
        
        RemoveOldValueFromMap();
    }
}















