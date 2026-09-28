namespace Game.Domain; 

public class Storage: StorageBuilding
{
    public Storage(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData,
    StorageBuildingDefinition definition) 
    : base(id, type, position, buildingData, definition) {}
}