namespace Game.Domain;

public abstract class BuildingDefinition
{
    public GridSize Size { get; }

    public BuildingDefinition(GridSize size)
    {
        Size = size;
    }
}






