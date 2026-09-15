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
    public BuildingType Type { get; set; }
    public GridSize Size { get; set; }
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
}

// factory class
class Factory
{
    private Dictionary<int, Building> _buildings;
    private Dictionary<GridPosition, int> _occupancy;
}