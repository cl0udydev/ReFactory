using System.Collections.Generic;

namespace Game.Domain;

public class RecipeDatabase
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