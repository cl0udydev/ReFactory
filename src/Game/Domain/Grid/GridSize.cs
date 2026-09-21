using System;

namespace Game.Domain;

public readonly record struct GridSize
{
    public int X { get; }
    public int Y { get; }

    public GridSize(int x, int y)
    {
        if (x <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if (y <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }
        
        this.X = x;
        this.Y = y;
    }
}