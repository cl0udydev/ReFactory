// resources
using System;
using System.Collections.Generic;
using Godot;

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

    public Factory()
    {
        _buildings = new();
        _occupancy = new();
    }

    private bool CanPlaceBuilding(BuildingType type, GridPosition position)
    {
        var size = _definitions.GetDefinition(type).Size;
        var currentPos = new GridPosition(0, 0);

        for (int offsetX = 0; offsetX < size.X; offsetX++)
        {
            for (int offsetY = 0; offsetY < size.Y; offsetY++)
            {
                currentPos.X = position.X + offsetX; currentPos.Y = position.Y + offsetY;

                if (_occupancy.ContainsKey(currentPos))
                {
                    return false;
                }
            }
        }
        return true;

    }
}