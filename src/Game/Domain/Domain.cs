using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
#nullable enable

// resources
enum ResourceType
{
    IronOre,
    IronPlate,
}

struct ResourceAmount
{
    public readonly ResourceType Type;
    public readonly int Amount;

    public ResourceAmount(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

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
    public readonly GridPosition Position;
    public readonly RecipeId SelectedRecipe;

    public Building(int id, BuildingType type, GridPosition position, RecipeId recipe)
    {
        this.Id = id;
        this.Type = type;
        this.Position = position;
        this.SelectedRecipe = recipe;
    }
}

struct BuildingDefinition
{
    public readonly GridSize Size;
    public readonly ImmutableArray<RecipeId> AllowedRecipes;
    public readonly int Capacity;
    public readonly double CraftSpeed;

    public BuildingDefinition(GridSize size, RecipeId[] recipes, int capacity, double craftSpeed)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        if (craftSpeed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(craftSpeed));
        }

        this.Size = size;
        this.AllowedRecipes = ImmutableArray.Create(recipes);
        this.Capacity = capacity;
        this.CraftSpeed = craftSpeed;
    }
}


class BuildingDefinitions
{
    private readonly Dictionary<BuildingType, BuildingDefinition> _definitions;

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
    
    public bool IsRecipeAllowed(BuildingType type, RecipeId recipeId)
    {
        if (!_definitions.TryGetValue(type, out BuildingDefinition def))
        {
            return false;
        }

        foreach (RecipeId id in def.AllowedRecipes)
        {
            if (id == recipeId)
            {
                return true;
            }
        }
        return false;
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
    public readonly ImmutableArray<ResourceAmount> Input;
    public readonly ImmutableArray<ResourceAmount> Output;
    public readonly double BaseCraftTime;

    public Recipe(ResourceAmount[] input, ResourceAmount[] output, double time)
    {
        if (time <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }
        
        this.Input = ImmutableArray.Create(input);
        this.Output = ImmutableArray.Create(output);
        this.BaseCraftTime = time;
    }
}

class RecipeDatabase
{
    private readonly Dictionary<RecipeId, Recipe> _recipes;

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
    public readonly int X;
    public readonly int Y;

    public GridPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

struct GridSize
{
    public readonly int X;
    public readonly int Y;

    public GridSize(int x, int y)
    {
        if (x <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if (y <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }
        
        this.X = x;
        this.Y = y;
    }
}

// inventory class
class Inventory
{
    private readonly Dictionary<ResourceType, int> _amounts;
    public readonly int Capacity;

    public Inventory(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        _amounts = new();
        Capacity = capacity;
    }

    public int Add(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        int currentAmount = GetAmount(type);
        int freeAmount = Capacity - currentAmount;

        int added = Math.Min(amount, freeAmount);
        _amounts[type] = currentAmount + added;

        return added;

    }

    public int Remove(ResourceType type, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        int currentAmount = GetAmount(type);

        int removed = Math.Min(amount, currentAmount);
        _amounts[type] = currentAmount - removed;

        return removed;
    }

    public int GetAmount(ResourceType type)
    {
        if (!_amounts.TryGetValue(type, out int amount))
        {
            return 0;
        }
        return amount;
    }
}


// factory class
class Factory
{
    private readonly Dictionary<int, Building> _buildings;
    private readonly Dictionary<GridPosition, int> _occupancy;
    private readonly BuildingDefinitions _definitions;
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

    public Building? PlaceBuilding(BuildingType type, GridPosition position, RecipeId recipeId)
    {
        if (!CanPlaceBuilding(type, position) || !_definitions.IsRecipeAllowed(type, recipeId))
        {
            return null;
        }

        Building building = new Building(_nextBuildingId, type, position, recipeId);

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