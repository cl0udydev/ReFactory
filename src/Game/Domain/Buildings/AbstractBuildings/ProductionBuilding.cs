using System;

namespace Game.Domain;

public abstract class ProductionBuilding : Building
{
    public Inventory InputInventory { get; }
    public Inventory OutputInventory { get; }
    public double Progress { get; set; }
    public RecipeId SelectedRecipe { get; }

    protected ProductionBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData) 
    : base(id, type, position, buildingData)
    {
        SelectedRecipe = buildingData.SelectedRecipe ?? throw new ArgumentNullException(nameof(buildingData.SelectedRecipe));
        int capacity = buildingData.Capacity ?? throw new ArgumentNullException(nameof(buildingData.Capacity));
        
        InputInventory = new Inventory(capacity);
        OutputInventory = new Inventory(capacity);
    }
}