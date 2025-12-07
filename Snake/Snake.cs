namespace SnakeGame;

public partial class Snake
{
    public int HeadX;
    public int HeadY;
    public Directions Direction { get; set; }
    public Snake(int x, int y, Directions direction)
    {
        HeadX = x;
        HeadY = y;
        Direction = direction;
    }
}