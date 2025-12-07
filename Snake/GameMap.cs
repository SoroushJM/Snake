namespace SnakeGame;


/// <summary>
/// a class that handels Map of game
/// a 2d arrays of cells
/// </summary>
public class GameMap
{
    public List<Snake> Snakes { get; }

    public List<List<Cell>> _Map { get; }

    public int AppelCount { get; set; }

    public GameMap(List<List<Cell>> map, List<Snake> snakes)
    {
        Snakes = snakes;
        _Map = map;
        foreach (var row in map)
        {
            foreach (var cell in row)
            {
                foreach (var snakeobj in snakes)
                {
                    cell.SnakeBodyNumbers.Add(new SnakeBodyNumber(snakeobj, 0));
                }
            }
        }

        InitialSnakes(snakes);

    }
    /// <summary>
    /// will place snakes on the map
    /// </summary>
    public void InitialSnakes(List<Snake> snakeObjs)
    {
        foreach (var snake in snakeObjs)
        {
            var x = snake.HeadX;
            var y = snake.HeadY;

            var listOfSnakeValue = _Map[x][y].SnakeBodyNumbers;
            for (int i = 0; i < listOfSnakeValue.Count; i++)
            {
                if (listOfSnakeValue[i]._SnakeRef == snake)
                {
                    listOfSnakeValue[i]._SnakeBodyValue = 1;
                }
            }
        }
    }



}
/// <summary>
/// A list of Snakes that will exist in this cell
/// with thier value
/// </summary>
public class Cell
{
    public required bool IsWall = false;
    public required List<SnakeBodyNumber> SnakeBodyNumbers;

    /// <summary>
    /// true if this cell have appel on it
    /// </summary>
    public bool HaveAppel { get; set; }

    public SnakeBodyNumber? GetSnakeValues(Snake snakeRef)
    {
        foreach (var snakeValue in SnakeBodyNumbers)
        {
            if (snakeValue._SnakeRef == snakeRef)
            {
                return snakeValue;
            }
        }
        return null;
    }
}

/// <summary>
/// snake object itself and its value in that cell
/// </summary>
public class SnakeBodyNumber
{
    public Snake _SnakeRef { get; set; }
    public int _SnakeBodyValue { get; set; }
    public SnakeBodyNumber(Snake snakeRef, int? snakeBodyValue)
    {
        _SnakeBodyValue = snakeBodyValue ?? 0;
        _SnakeRef = snakeRef;
    }
}