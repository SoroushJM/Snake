namespace SnakeGame;

public static class MapMaker
{
    public static GameMap CreateDefaultMap(int xCount, int yCount)
    {
        List<List<Cell>> map = new List<List<Cell>>(xCount);
        map.AddRange(Enumerable.Repeat(new List<Cell>(), xCount));
        for (int i = 0; i < xCount; i++)
        {

            map[i] = new List<Cell>(yCount);
            for (int j = 0; j < yCount; j++)
            {
                if (i == 0 || j == 0 || i == xCount - 1 || j == yCount - 1)
                {
                    map[i].Add(new Cell() { IsWall = true, SnakeBodyNumbers = [] });
                }
                else
                {
                    map[i].Add(new Cell() { IsWall = false, SnakeBodyNumbers = [] });
                }
            }
        }
        return new GameMap(map, [new(xCount / 2, yCount / 2, Snake.Directions.right)]);
    }
}
