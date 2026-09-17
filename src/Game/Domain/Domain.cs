using System.Collections.Generic;
#nullable enable

// resources
enum ResourceType
{
    IronOre,
    IronPlate,
}

struct ResourceAmount
{
    public ResourceType Type;
    public int Amount;

    public ResourceAmount(ResourceType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
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
    public readonly GridSize Size;
    public readonly RecipeId[] AllowedRecipes;
    public readonly int Capacity;
    public readonly double CraftSpeed;

    public BuildingDefinition(GridSize size, RecipeId[] recipes, int capacity, double craftSpeed)
    {
        this.Size = size;
        this.AllowedRecipes = recipes;
        this.Capacity = capacity;
        this.CraftSpeed = craftSpeed;
    }
}


class BuildingDefinitions
{
    private Dictionary<BuildingType, BuildingDefinition> _definitions;

    public BuildingDefinitions()
    {
        _definitions = new()
        {
            [BuildingType.Furnace] = new BuildingDefinition(
                size: new GridSize(2, 3),
                recipes: new RecipeId[] {RecipeId.IronPlate},
                capacity: 50,
                craftSpeed: 0.75
            )
        };
    }

    public BuildingDefinition GetDefinition(BuildingType type)
    {
        return _definitions[type];
    }
}

// recipes
enum RecipeId
{
    IronPlate,
    CopperPlate,
}

struct Recipe
{
    public ResourceAmount[] Input;
    public ResourceAmount[] Output;
    public double BaseCraftTime;

    public Recipe(ResourceAmount[] input, ResourceAmount[] output, double time)
    {
        this.Input = input;
        this.Output = output;
        this.BaseCraftTime = time;
    }
}

class RecipeDatabase
{
    private Dictionary<RecipeId, Recipe> _recipes;

    public RecipeDatabase()
    {
        _recipes = new();

        _recipes[RecipeId.IronPlate] = new Recipe(
            input: new ResourceAmount[]
            {
                new ResourceAmount(ResourceType.IronOre, 2),
            },
            output: new ResourceAmount[]
            {
                new ResourceAmount(ResourceType.IronPlate, 1),
            },
            time: 5.0
        );
    }

    public Recipe GetRecipe(RecipeId id)
    {
        return _recipes[id];
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

    public Building? PlaceBuilding(BuildingType type, GridPosition position)
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

    public void RemoveBuilding(int id)
    {        
        if (!_buildings.TryGetValue(id, out Building? building))
        {
            return;
        }

        GridPosition position = building.Position;
        BuildingType type = building.Type;
        GridSize size = _definitions.GetDefinition(type).Size;

        for (int offsetX = 0; offsetX < size.X; offsetX++)
        {
            for (int offsetY = 0; offsetY < size.Y; offsetY++)
            {
                GridPosition grid = new GridPosition(position.X + offsetX, position.Y + offsetY);

                _occupancy.Remove(grid);
            }
        }
        
        _buildings.Remove(id);

        
    }

    public Building? GetBuildingAt(GridPosition position)
    {
        if (!_occupancy.TryGetValue(position, out int id))
        {
            return null;
        }

        Building building = _buildings[id];

        return building;
    }

    public Building? GetBuilding(int id)
    {
        if (!_buildings.TryGetValue(id, out Building? building))
        {
            return null;
        }
        return building;
    }

    public int BuildingCount()
    {
        return _buildings.Count;
    }
}