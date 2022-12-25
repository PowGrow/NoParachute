using System.Collections.Generic;
using UnityEngine;

public interface ISpriteProvider
{
    public Color BottomColor { get; }
    public List<Sprite> Decoratives { get; }
    public List<Sprite> Walls { get; }
    public List<int> WallsSpawnChance { get; }
}