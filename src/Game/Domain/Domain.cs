enum ResourceType
{
    IronOre,
    IronPlate,
}

struct ResourceStack
{
    public ResourceType Type { get; set; }
    public int Amount { get; set; }
}

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