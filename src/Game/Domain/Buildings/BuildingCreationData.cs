namespace Game.Domain;

public class BuildingCreationData
{
    public RecipeId? SelectedRecipe { get; }
    public Direction? Direction { get; }


    public BuildingCreationData(RecipeId? recipe, Direction? direction)
    {
        SelectedRecipe = recipe;
        Direction = direction;
    }
}