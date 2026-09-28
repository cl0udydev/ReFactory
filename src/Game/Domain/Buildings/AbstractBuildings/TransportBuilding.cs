using System;

namespace Game.Domain;

public abstract class TransportBuilding : Building
{
    public Direction Direction { get; }
    
    protected TransportBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData, 
    TransportBuildingDefinition definition) 
    : base(id, type, position)
    {
        Direction = buildingData.Direction ?? throw new ArgumentNullException(nameof(buildingData.Direction));
    }
}