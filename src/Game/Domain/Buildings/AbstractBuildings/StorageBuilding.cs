using System;

namespace Game.Domain;

public abstract class StorageBuilding : Building
{
    public Inventory Inventory { get; }
    protected StorageBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData) 
    : base(id, type, position, buildingData)
    {
        int capacity = buildingData.Capacity ?? throw new ArgumentNullException(nameof(buildingData.Capacity));
        
        Inventory = new Inventory(capacity);
    }
}