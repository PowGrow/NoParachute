using System.Collections.Generic;
using UnityEngine;

public interface ISpriteProvider
{
    List<Sprite> Decoratives { get; }
    int LevelId { get; }
    List<Sprite> Obstacles { get; }
    List<Sprite> Walls { get; }
    List<int> WallsSpawnChance { get; }
}