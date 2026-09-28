namespace Game.Domain; 

public class Furnace: ProductionBuilding
{
    public Furnace(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData,
    ProductionBuildingDefinition definition) 
    : base(id, type, position, buildingData, definition) {}
}