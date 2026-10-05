namespace Game.Domain;

public class BuildingCreationData
{
    public RecipeId? SelectedRecipe { get; }
    public ResourceType? ResourceType { get; }
    public Direction? Direction { get; }


    public BuildingCreationData(RecipeId? recipe, Direction? direction, ResourceType? resourceType)
    {
        SelectedRecipe = recipe;
        Direction = direction;
        ResourceType = resourceType;
    }
}