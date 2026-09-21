namespace Game.Domain;

public class Building
{
    public int Id { get; }
    public BuildingType Type { get; }
    public GridPosition Position { get; }
    public RecipeId SelectedRecipe { get; }

    public Building(int id, BuildingType type, GridPosition position, RecipeId recipe)
    {
        this.Id = id;
        this.Type = type;
        this.Position = position;
        this.SelectedRecipe = recipe;
    }
}