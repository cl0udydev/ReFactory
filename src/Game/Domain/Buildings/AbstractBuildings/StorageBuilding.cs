using System;

namespace Game.Domain;

public abstract class StorageBuilding : Building
{
    public Inventory Inventory { get; }
    
    protected StorageBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData,
    StorageBuildingDefinition definition) 
    : base(id, type, position)
    {
        int capacity = definition.Capacity;
        
        Inventory = new Inventory(capacity);
    }
}