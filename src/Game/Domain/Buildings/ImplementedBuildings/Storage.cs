namespace Game.Domain; 

public class Storage: StorageBuilding
{
    public Storage(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData) 
    : base(id, type, position, buildingData) {}
}