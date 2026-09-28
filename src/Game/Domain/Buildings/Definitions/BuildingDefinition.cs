using System;

namespace Game.Domain;

public abstract class BuildingDefinition
{
    public GridSize Size { get; }
    public Type BuildingClass { get; }

    protected BuildingDefinition(Type buildingClass, GridSize size)
    {
        if (buildingClass == null) 
        {
            throw new ArgumentNullException(nameof(buildingClass));
        }
        if (buildingClass.IsAbstract || !typeof(Building).IsAssignableFrom(buildingClass)) 
        {
            throw new ArgumentException(nameof(buildingClass));
        }

        Size = size;
        BuildingClass = buildingClass;
    }
}