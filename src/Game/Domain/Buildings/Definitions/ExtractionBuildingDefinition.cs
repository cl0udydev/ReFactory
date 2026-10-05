using System;

namespace Game.Domain;

public class ExtractionBuildingDefinition: BuildingDefinition
{
    public int Capacity { get; }
    public double ExtractionSpeed { get; }

    public ExtractionBuildingDefinition(Type buildingClass, GridSize size, int capacity, double speed) : base(buildingClass, size)
    {
        Capacity = capacity;
        ExtractionSpeed = speed;
    }
}