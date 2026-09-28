using System;

namespace Game.Domain;

public class TransportBuildingDefinition: BuildingDefinition
{
    public int TransferSpeed { get; }

    public TransportBuildingDefinition(Type buildingClass, GridSize size, int transferSpeed) : base(buildingClass, size)
    {
        if (transferSpeed <= 0) throw new ArgumentOutOfRangeException(nameof(transferSpeed));
        
        TransferSpeed = transferSpeed;
    }
}