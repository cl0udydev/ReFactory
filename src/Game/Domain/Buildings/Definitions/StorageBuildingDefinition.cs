using System;

namespace Game.Domain;

public class StorageBuildingDefinition: BuildingDefinition
{
    public int Capacity { get; }

    public StorageBuildingDefinition(Type buildingClass, GridSize size, int capacity) 
    : base(buildingClass, size)
    {
        if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));

        Capacity = capacity;
    }
}