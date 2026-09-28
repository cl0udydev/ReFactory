namespace Game.Domain;

public class BuildingCreationData
{
    public RecipeId? SelectedRecipe { get; }
    public Direction? Direction { get; }
    public int? Capacity { get; private set; }


    public BuildingCreationData(RecipeId? recipe, Direction? direction)
    {
        SelectedRecipe = recipe;
        Direction = direction;
    }

    public void CompleteFromDefinition(BuildingDefinition definition)
    {
        if (definition is ProductionBuildingDefinition prodDef)
        {
            Capacity = prodDef.Capacity;
        }

    }
}