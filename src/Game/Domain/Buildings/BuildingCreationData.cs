namespace Game.Domain;

public class BuildingCreationData
{
    public RecipeId? SelectedRecipe { get; }

    public int? Capacity { get; private set; }

    public BuildingCreationData(RecipeId? recipe)
    {
        SelectedRecipe = recipe;
    }

    public void ExpansionData(BuildingDefinition definition)
    {
        if (definition is ProductionBuildingDefinition prodDef)
        {
            Capacity = prodDef.Capacity;
        }
    }
}