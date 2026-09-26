namespace Game.Domain;

public class StorageBuildingDefinition: BuildingDefinition
{
    public int Capacity { get; }

    public StorageBuildingDefinition(GridSize size, int capacity) : base(size)
    {
        Capacity = capacity;
    }
}