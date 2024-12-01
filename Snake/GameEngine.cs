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
    Task _ConsoleHandler;
    List<SnakeObj> _SnakeObjs { get; set; }
    ConsoleManger _consoleManger;
    GameState _gameState = new();
    DateTime lasTickTime = DateTime.Now;
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
        Task? consoleHandler = Task.Run(consoleManger.HandelInput);
        _ConsoleHandler = consoleHandler;
    }

    public async Task MainGameLoop()
    {
        _ConsoleHandler = Task.Run(() => _consoleManger.HandelInput());
        while (true)
        {

            UpdateSnakesWhoEatAppel();

            AddAppelIfDoesntExist();

            UpdateSnakes();

            CheckGameState(_gameState);
            if(_gameState._GameLost == true)

            _consoleManger.Draw();

            var howMuchPastFromLastTick = (lasTickTime - DateTime.Now).Seconds;
            if (1000 / 4 > howMuchPastFromLastTick)
            {
                await Task.Delay(1000 / 4 - howMuchPastFromLastTick);
            }
            lasTickTime = DateTime.Now;
        }
    }

    public void W_Key_Pressed(object? sender, EventArgs e)
    {
        if (_SnakeObjs[0].Direction != SnakeObj.Directions.down)
            _SnakeObjs[0].Direction = SnakeObj.Directions.up;
    }
    public void S_Key_Pressed(object? sender, EventArgs e)
    {
        if (_SnakeObjs[0].Direction != SnakeObj.Directions.up)
            _SnakeObjs[0].Direction = SnakeObj.Directions.down;
    }
    public void A_Key_Pressed(object? sender, EventArgs e)
    {
        if (_SnakeObjs[0].Direction != SnakeObj.Directions.right)
            _SnakeObjs[0].Direction = SnakeObj.Directions.left;
    }
    public void D_Key_Pressed(object? sender, EventArgs e)
    {
        if (_SnakeObjs[0].Direction != SnakeObj.Directions.left)
            _SnakeObjs[0].Direction = SnakeObj.Directions.right;
    }


    public void MoveUpSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;

        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeBodyValue;
        _Map[x - 1][y].GetSnakeValues(snakeObj)!._SnakeBodyValue = currentHeadValue + 1;


        snakeObj.HeadX = x - 1;
        snakeObj.HeadY = y;

    }
    public void MoveDownSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;

        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeBodyValue;
        _Map[x + 1][y].GetSnakeValues(snakeObj)!._SnakeBodyValue = currentHeadValue + 1;


        snakeObj.HeadX = x + 1;
        snakeObj.HeadY = y;
    }
    public void MoveRightSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;
        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeBodyValue;
        _Map[x][y + 1].GetSnakeValues(snakeObj)!._SnakeBodyValue = currentHeadValue + 1;


        snakeObj.HeadX = x;
        snakeObj.HeadY = y + 1;
    }
    public void MoveLeftSnake(SnakeObj snakeObj)
    {
        var x = snakeObj.HeadX;
        var y = snakeObj.HeadY;
        var currentHeadValue = _Map[x][y].GetSnakeValues(snakeObj)!._SnakeBodyValue;
        _Map[x][y - 1].GetSnakeValues(snakeObj)!._SnakeBodyValue = currentHeadValue + 1;


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

    
    /// <summary>
    /// decrease 1 number from body
    /// </summary>
    public void RemoveOldValueFromMap()
    {
        foreach (var row in _Map)
        {
            foreach (var cell in row)
            {
                foreach (var snakeValues in cell.SnakesValues)
                {
                    if (snakeValues._SnakeBodyValue > 0)
                        snakeValues._SnakeBodyValue--;
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
            while (!IsValidPlace4Appel(x, y));

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
            if (snakeVal._SnakeBodyValue > 0)
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

                SnakeBodyNumber x = cell.SnakesValues.Find(x => x._SnakeRef == snake)!;

                x._SnakeBodyValue++;

            }

        }
    }

    public void UpdateSnakes()
    {
        MoveSnakes();

        RemoveOldValueFromMap();
    }

    public void CheckGameState(GameState gameState)
    {
        gameState ??= new GameState();
        foreach (var row in _Map)
        {
            foreach (Cell cell in row)
            {
                if (cell.IsWall == true &&
                    cell.SnakesValues.Any(snakeBodyNumber => snakeBodyNumber._SnakeBodyValue > 0))
                {
                    gameState._GameLost = true;
                    gameState.Reasons.Add(GameState.Reason.SnakeHitTheWall);
                }
            }
        }
        
    }
    public class GameState
    {
        public bool _GameLost = false;
        public List<Reason> Reasons = new List<Reason>();
        public GameState()
        {
            _GameLost = false;
        }

        public GameState(bool isGameLost,Reason reason = Reason.None)
        {
            _GameLost = isGameLost;
            if (reason != Reason.None)
            {
                Reasons.Add(reason);
            }
        }

        public enum Reason{
            None,
            SnakeHitTheWall
        }
    }
}










