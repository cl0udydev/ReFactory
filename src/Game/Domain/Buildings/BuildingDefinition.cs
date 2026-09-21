using System;
using System.Collections.Immutable;

namespace Game.Domain;

public readonly struct BuildingDefinition
{
    public GridSize Size { get; }
    public ImmutableArray<RecipeId> AllowedRecipes { get; }
    public int Capacity { get; }
    public double CraftSpeed { get; }

    public BuildingDefinition(GridSize size, RecipeId[] recipes, int capacity, double craftSpeed)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        if (craftSpeed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(craftSpeed));
        }

        this.Size = size;
        this.AllowedRecipes = ImmutableArray.Create(recipes);
        this.Capacity = capacity;
        this.CraftSpeed = craftSpeed;
    }
}
