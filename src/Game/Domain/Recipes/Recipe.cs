using System;
using System.Collections.Immutable;

namespace Game.Domain;

public struct Recipe
{
    public readonly ImmutableArray<ResourceAmount> Input;
    public readonly ImmutableArray<ResourceAmount> Output;
    public readonly double BaseCraftTime;

    public Recipe(ResourceAmount[] input, ResourceAmount[] output, double time)
    {
        if (input == null)  throw new ArgumentNullException(nameof(input));
        if (output == null) throw new ArgumentNullException(nameof(output));
        if (time <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }
        
        this.Input = ImmutableArray.Create(input);
        this.Output = ImmutableArray.Create(output);
        this.BaseCraftTime = time;
    }
}