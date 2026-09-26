using System;

namespace Game.Domain;

public class StorageBuildingDefinition: BuildingDefinition
{
    public int Capacity { get; }

    public StorageBuildingDefinition(Type buildingClass, GridSize size, int capacity) 
    : base(size, buildingClass)
    {
        Capacity = capacity;
    }
}