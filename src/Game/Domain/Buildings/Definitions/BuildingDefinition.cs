using System;

namespace Game.Domain;

public abstract class BuildingDefinition
{
    public GridSize Size { get; }
    public Type BuildingClass { get; }

    protected BuildingDefinition(Type buildingClass, GridSize size)
    {
        Size = size;

        if (!buildingClass.IsAssignableTo(typeof(Building)))
            {
                throw new ArgumentException(nameof(buildingClass));
            }


        BuildingClass = buildingClass;
    }

}






