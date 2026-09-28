namespace Game.Domain; 

public class Conveyor: TransportBuilding
{
    public Conveyor(int id, BuildingType type, GridPosition position, BuildingCreationData buildingData, 
    TransportBuildingDefinition definition)
    : base(id, type, position, buildingData, definition) {}
}