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