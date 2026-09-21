using System;

namespace Game.Domain;

public readonly record struct ResourceAmount
{
    public ResourceType Type { get; }
    public int Amount { get; }

    public ResourceAmount(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException(nameof(amount));
        }

        Type = type;
        Amount = amount;
    }
}