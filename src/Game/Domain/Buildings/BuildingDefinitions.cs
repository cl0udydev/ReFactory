using System.Collections.Generic;

namespace Game.Domain;

public class BuildingDefinitions
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

    public bool TryGetDefinition(BuildingType type, out BuildingDefinition definition)
    {
        return _definitions.TryGetValue(type, out definition);
    }
    
    public bool IsRecipeAllowed(BuildingType type, RecipeId recipeId)
    {
        return _definitions.TryGetValue(type, out BuildingDefinition def) && def.AllowedRecipes.Contains(recipeId);
    }
}