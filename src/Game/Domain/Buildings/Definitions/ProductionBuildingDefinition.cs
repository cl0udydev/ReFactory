using System;
using System.Collections.Immutable;

namespace Game.Domain;

public class ProductionBuildingDefinition: BuildingDefinition
{
    public ImmutableArray<RecipeId> AllowedRecipes { get; }
    public int Capacity { get; }
    public double CraftSpeed { get; }

    public ProductionBuildingDefinition(Type buildingClass, GridSize size, RecipeId[] recipes, int capacity, double craftSpeed) 
    : base(size, buildingClass)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        if (craftSpeed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(craftSpeed));
        }

        AllowedRecipes = ImmutableArray.Create(recipes);
        Capacity = capacity;
        CraftSpeed = craftSpeed;
    }
}