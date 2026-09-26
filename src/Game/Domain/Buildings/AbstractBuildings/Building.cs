namespace Game.Domain;

public abstract class Building
{
    public int Id { get; }
    public BuildingType Type { get; }
    public GridPosition Position { get; }
    

    protected Building(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData)
    {
        Id = id;
        Type = type;
        Position = position;
    }
}

