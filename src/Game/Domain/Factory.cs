using System;
using System.Collections.Generic;
#nullable enable

namespace Game.Domain;

public class Factory
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

    private bool CanPlaceBuilding(BuildingDefinition definition, GridPosition position)
    {
        GridSize size = definition.Size;

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

    public PlaceBuildingResult PlaceBuilding(BuildingType type, GridPosition position, BuildingCreationData buildingData)
    {
        if (!_definitions.TryGetDefinition(type, out BuildingDefinition definition))
        {
            return new PlaceBuildingResult(PlaceBuildingResultType.UnknownBuildingType);
        }
        // if (!_definitions.IsRecipeAllowed(type, recipeId))
        // {
        //     return new PlaceBuildingResult(PlaceBuildingResultType.InvalidRecipe);
        // }
        if (!CanPlaceBuilding(definition, position))
        {
            return new PlaceBuildingResult(PlaceBuildingResultType.Occupied);
        }

        buildingData.CompleteFromDefinition(definition);

        Building building = (Building)Activator.CreateInstance(
            _definitions.GetBuildingClass(type), _nextBuildingId, type, position, buildingData
            )!;

        _buildings.Add(_nextBuildingId, building);
        
        GridSize size = definition.Size;

        for (int offsetX = 0; offsetX < size.X; offsetX++)
        {
            for (int offsetY = 0; offsetY < size.Y; offsetY++)
            {
                GridPosition grid = new GridPosition(position.X + offsetX, position.Y + offsetY);

                _occupancy.Add(grid, _nextBuildingId);
            }
        }

        _nextBuildingId++;

        return new PlaceBuildingResult(PlaceBuildingResultType.Success, building);
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