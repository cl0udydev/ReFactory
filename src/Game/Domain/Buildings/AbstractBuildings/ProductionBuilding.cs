using System;

namespace Game.Domain;

public abstract class ProductionBuilding : Building
{
    public Inventory InputInventory { get; }
    public Inventory OutputInventory { get; }
    public double Progress { get; internal set; }
    public RecipeId SelectedRecipe { get; }

    protected ProductionBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData, 
    ProductionBuildingDefinition definition)
    : base(id, type, position)
    {
        SelectedRecipe = buildingData.SelectedRecipe ?? throw new ArgumentNullException(nameof(buildingData.SelectedRecipe));
        int capacity = definition.Capacity;
        
        InputInventory = new Inventory(capacity);
        OutputInventory = new Inventory(capacity);
    }
}