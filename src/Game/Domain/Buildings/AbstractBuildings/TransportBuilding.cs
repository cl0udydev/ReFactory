namespace Game.Domain;

public abstract class TransportBuilding : Building
{
    protected TransportBuilding(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData) 
    : base(id, type, position, buildingData) {}
}