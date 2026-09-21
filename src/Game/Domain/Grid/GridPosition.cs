namespace Game.Domain;

public readonly record struct GridPosition
{
    public int X { get; }
    public int Y { get; }

    public GridPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}