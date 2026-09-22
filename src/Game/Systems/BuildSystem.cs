using Game.Domain;

namespace Game.Systems;

public class BuildSystem
{
    private readonly Factory _factory; 

    public BuildSystem(Factory factory)
    {
        _factory = factory;
    }

    public PlaceBuildingResult Build(BuildingType type, GridPosition position, RecipeId recipeId)
    {
        return _factory.PlaceBuilding(type, position, recipeId);
    }
}