// resources
using System.Collections.Generic;

enum ResourceType
{
    IronOre,
    IronPlate,
}

struct ResourceStack
{
    public ResourceType Type { get; set; }
    public int Amount { get; set; }
}

// buildings
enum BuildingType
{
    Mine,
    Furnace,
    Conveyor,
    Storage,
}

class Building
{
    public int Id { get; set; }
    public BuildingType Type { get; set; }
    public GridPosition Position { get; set; }
}

struct BuildingDefinition
{
    public GridSize Size { get; set; }

    public BuildingDefinition(GridSize size)
    {
        this.Size = size;
    }
}


class BuildingDefinitions
{
    private Dictionary<BuildingType, BuildingDefinition> _definitions;

    public BuildingDefinitions()
    {
        _definitions = new()
        {
            [BuildingType.Furnace] = new BuildingDefinition(new GridSize(2, 3))
        };
    }

    public BuildingDefinition GetDefinition(BuildingType type)
    {
        return _definitions[type];
    }
}


// grid position and grid size structs
struct GridPosition
{
    public int X { get; set; }
    public int Y { get; set; }
}

struct GridSize
{
    public int X { get; set; }
    public int Y { get; set; }

    public GridSize(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

// factory class
class Factory
{
    private Dictionary<int, Building> _buildings;
    private Dictionary<GridPosition, int> _occupancy;

    public Factory()
    {
        _buildings = new();
        _occupancy = new();
    }

    private bool CanPlaceBuilding(BuildingType type, GridPosition position)
    {
        
    }
}