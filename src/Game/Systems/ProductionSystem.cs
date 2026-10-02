using Game.Domain;

namespace Game.Systems;

public class ProductionSystem
{
    private readonly Factory _factory;
    private readonly BuildingDefinitions _definitions;
    private readonly RecipeDatabase _recipesDatabase;

    public ProductionSystem(Factory factory, BuildingDefinitions definitions, RecipeDatabase recipeDatabase)
    {
        _factory = factory;
        _definitions = definitions;
        _recipesDatabase = recipeDatabase;
    }

    public void Update(double deltaTime)
    {
        var productionBuildings = _factory.GetBuildingsAtType(typeof(ProductionBuilding));

        foreach (ProductionBuilding prodBuilding in productionBuildings)
        {
            var definition = (ProductionBuildingDefinition)_definitions.GetDefinition(prodBuilding.Type);
            var recipe = _recipesDatabase.GetRecipe(prodBuilding.SelectedRecipe);
            var actualCraftTime = recipe.BaseCraftTime / definition.CraftSpeed;
            var canProduce = true;

            foreach (ResourceAmount input in recipe.Input)
            {
                var available = prodBuilding.InputInventory.GetAmount(input.Type);

                if (available < input.Amount)
                {
                    canProduce = false;
                    break;
                }
            }

            var totalOutputAmount = 0;

            foreach (ResourceAmount output in recipe.Output)
            {
                totalOutputAmount += output.Amount;
            }
            
            var freeCapacity = prodBuilding.OutputInventory.Capacity - prodBuilding.OutputInventory.GetTotalAmount();

            if (freeCapacity < totalOutputAmount) canProduce = false;

            if (!canProduce) return;

            prodBuilding.Progress += deltaTime;

            while (prodBuilding.Progress >= actualCraftTime)
            {
                var canCompleteCycle = true;

                foreach (ResourceAmount input in recipe.Input)
                {
                    var available = prodBuilding.InputInventory.GetAmount(input.Type);

                    if (available < input.Amount)
                    {
                        canCompleteCycle = false;
                        break;
                    }
                }

                freeCapacity = prodBuilding.OutputInventory.Capacity - prodBuilding.OutputInventory.GetTotalAmount();

                if (freeCapacity < totalOutputAmount)
                {
                    canCompleteCycle = false;
                }

                if (!canCompleteCycle)
                {
                    prodBuilding.Progress = actualCraftTime;
                    break;
                }

                foreach (ResourceAmount input in recipe.Input)
                {
                    prodBuilding.InputInventory.Remove(input.Type, input.Amount);
                }

                foreach (ResourceAmount output in recipe.Output)
                {
                    prodBuilding.OutputInventory.Add(output.Type, output.Amount);
                }

                prodBuilding.Progress -= actualCraftTime;
            }          

        }
    }
}