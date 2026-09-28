using System;

namespace Game.Domain;

public abstract class TransportBuilding : Building
{
    public Direction Direction { get; }
    
    protected TransportBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData) 
    : base(id, type, position, buildingData)
    {
        Direction = buildingData.Direction ?? throw new ArgumentNullException(nameof(buildingData.Direction));
    }
}