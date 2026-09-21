using System;
using System.Collections.Generic;

namespace Game.Domain;

public class Inventory
{
    private readonly Dictionary<ResourceType, int> _amounts;
    public int Capacity { get; }

    public Inventory(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        _amounts = new();
        Capacity = capacity;
    }

    public int Add(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        int currentAmount = GetAmount(type);
        int freeAmount = Capacity - currentAmount;

        int added = Math.Min(amount, freeAmount);
        _amounts[type] = currentAmount + added;

        return added;

    }

    public int Remove(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        int currentAmount = GetAmount(type);

        int removed = Math.Min(amount, currentAmount);
        _amounts[type] = currentAmount - removed;

        return removed;
    }

    public int GetAmount(ResourceType type)
    {
        if (!_amounts.TryGetValue(type, out int amount))
        {
            return 0;
        }
        return amount;
    }
}