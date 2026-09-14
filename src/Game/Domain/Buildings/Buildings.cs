enum BuildingType
{
    Mine,
    Furnace,
    Conveyor,
    Storage,
}

struct Building
{
    public int Id { get; set; }
    public BuildingType Type { get; set; }
}