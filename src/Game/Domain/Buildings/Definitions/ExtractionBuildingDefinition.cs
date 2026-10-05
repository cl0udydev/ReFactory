using System;

namespace Game.Domain;

public class ExtractionBuildingDefinition: BuildingDefinition
{
    public int Capacity { get; }
    public double ExtractionSpeed { get; }

    public ExtractionBuildingDefinition(Type buildingClass, GridSize size, int capacity, double extractSpeed) : base(buildingClass, size)
    {
        if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        if (extractSpeed <= 0) throw new ArgumentOutOfRangeException(nameof(extractSpeed));
        
        Capacity = capacity;
        ExtractionSpeed = extractSpeed;
    }
}