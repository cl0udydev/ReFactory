using System;

namespace Game.Domain;

public abstract class ExtractionBuilding : Building
{
    public Inventory OutputInventory { get; }
    public double Progress { get; internal set; }
    public ResourceType ResourceType { get; } 

    protected ExtractionBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData, 
    ExtractionBuildingDefinition definition)
    : base(id, type, position)
    {
        ResourceType = buildingData.ResourceType ?? throw new ArgumentNullException(nameof(buildingData.ResourceType));
        int capacity = definition.Capacity;
        
        OutputInventory = new Inventory(capacity);
    }
}