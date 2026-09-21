using System;
#nullable enable

namespace Game.Domain;

public readonly struct PlaceBuildingResult
{
    public PlaceBuildingResultType Type { get; }
    public Building? Building { get; }
    public PlaceBuildingResult(PlaceBuildingResultType type)
    {
        if (type == PlaceBuildingResultType.Success)
        {
            throw new ArgumentException(nameof(type));
        }
        Type = type;
        Building = null;
    }

    public PlaceBuildingResult(PlaceBuildingResultType type, Building building)
    {
        if (type != PlaceBuildingResultType.Success)
        {
            throw new ArgumentException(nameof(type));
        }
        if (building == null)
        {
            throw new ArgumentNullException(nameof(building));
        }
        Type = type;
        Building = building;      
    }
}