using System.Collections.Generic;
using UnityEngine;

public interface ISpriteProvider
{
    List<Sprite> Decoratives { get; }
    List<Sprite> Walls { get; }
    List<int> WallsSpawnChance { get; }
}