using System;
using System.Collections.Generic;

namespace Game.Domain;

public class BuildingDefinitions
{
    private readonly Dictionary<BuildingType, BuildingDefinition> _definitions;
    private readonly Dictionary<BuildingType, Type> _buildingsClasses;

    public BuildingDefinitions()
    {
        _definitions = new()
        {
            [BuildingType.Furnace] = new ProductionBuildingDefinition(
                size: new GridSize(2, 3),
                recipes: new RecipeId[] {RecipeId.IronPlate},
                capacity: 50,
                craftSpeed: 0.75
            )
        };
        _buildingsClasses = new()
        {
            [BuildingType.Furnace] = typeof(Furnace)
        };
    }

    public BuildingDefinition GetDefinition(BuildingType type)
    {
        return _definitions[type];
    }

    public Type GetBuildingClass(BuildingType type)
    {
        return _buildingsClasses[type];
    }

    public bool TryGetDefinition(BuildingType type, out BuildingDefinition definition)
    {
        return _definitions.TryGetValue(type, out definition);
    }
    
    public bool IsRecipeAllowed(BuildingType type, RecipeId recipeId)
    {
        if (_definitions.TryGetValue(type, out BuildingDefinition def) && def is ProductionBuildingDefinition productionDef)
        {
            return productionDef.AllowedRecipes.Contains(recipeId);
        }

        return false;
    }
}