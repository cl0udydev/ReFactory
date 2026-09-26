using System;

namespace Game.Domain;

public abstract class BuildingDefinition
{
    public GridSize Size { get; }
    public Type BuildingClass { get; }

    public BuildingDefinition(GridSize size, Type buildingClass)
    {
        Size = size;

        if (!buildingClass.IsAssignableTo(typeof(Building)))
            {
                throw new ArgumentException(nameof(buildingClass));
            }


        BuildingClass = buildingClass;
    }

}






