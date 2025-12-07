namespace SnakeGame;

public class Application
{
    public static async Task Main()
    {
        var gameMap = MapMaker.CreateDefaultMap(10, 10);

        ConsoleManger consoleManger = new(gameMap, gameMap.Snakes);

        GameEngine gameEngine = new(consoleManger, gameMap, gameMap.Snakes);
        await gameEngine.MainGameLoop();

    }
}