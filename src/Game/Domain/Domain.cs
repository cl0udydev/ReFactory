// resources
using System.Collections.Generic;
#nullable enable

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
    public readonly int Id;
    public readonly BuildingType Type;
    public GridPosition Position { get; set; }

    public Building(int id, BuildingType type, GridPosition position)
    {
        this.Id = id;
        this.Type = type;
        this.Position = position;
    }
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

    public GridPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
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
    private BuildingDefinitions _definitions;
    private int _nextBuildingId = 1;

    public Factory()
    {
        _buildings = new();
        _occupancy = new();
        _definitions = new();
    }

    private bool CanPlaceBuilding(BuildingType type, GridPosition position)
    {
        GridSize size = _definitions.GetDefinition(type).Size;

        for (int offsetX = 0; offsetX < size.X; offsetX++)
        {
            for (int offsetY = 0; offsetY < size.Y; offsetY++)
            {

                GridPosition currentPos = new GridPosition(position.X + offsetX, position.Y + offsetY);

                if (_occupancy.ContainsKey(currentPos))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private Building? PlaceBuilding(BuildingType type, GridPosition position)
    {
        if (!CanPlaceBuilding(type, position))
        {
            return null;
        }

        Building building = new Building(_nextBuildingId, type, position);

        _buildings.Add(_nextBuildingId, building);
        
        GridSize size = _definitions.GetDefinition(type).Size;

        for (int offsetX = 0; offsetX < size.X; offsetX++)
        {
            for (int offsetY = 0; offsetY < size.Y; offsetY++)
            {
                GridPosition grid = new GridPosition(position.X + offsetX, position.Y + offsetY);

                _occupancy.Add(grid, _nextBuildingId);
            }
        }

        _nextBuildingId++;

        return building;
    }
}