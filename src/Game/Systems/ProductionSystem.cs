using System;
using System.Collections.Generic;
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
        List<Building> productionBuildings = _factory.GetBuildingsAtType(typeof(ProductionBuilding));

        foreach (ProductionBuilding prodBuilding in productionBuildings)
        {
            ProductionBuildingDefinition definition = (ProductionBuildingDefinition)_definitions.GetDefinition(prodBuilding.Type);
            Recipe recipe = _recipesDatabase.GetRecipe(prodBuilding.SelectedRecipe);
            double actualCraftTime = recipe.BaseCraftTime / definition.CraftSpeed;
            bool canProduce = true;

            foreach (ResourceAmount input in recipe.Input)
            {
                int available = prodBuilding.InputInventory.GetAmount(input.Type);

                if (available < input.Amount)
                {
                    canProduce = false;
                    break;
                }
            }

            int totalOutputAmount = 0;

            foreach (ResourceAmount output in recipe.Output)
            {
                totalOutputAmount += output.Amount;
            }
            
            int freeCapacity = prodBuilding.OutputInventory.Capacity - prodBuilding.OutputInventory.GetTotalAmount();

            if (freeCapacity < totalOutputAmount) canProduce = false;

            if (!canProduce) return;

            prodBuilding.Progress += deltaTime;

            while (prodBuilding.Progress >= actualCraftTime)
            {
                bool canCompleteCycle = true;

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