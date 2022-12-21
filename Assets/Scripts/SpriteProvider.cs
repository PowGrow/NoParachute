using System.Collections.Generic;
using UnityEngine;

public class SpriteProvider : ISpriteProvider, IProvider
{
    private LevelData _currentLevel;

    public Status Status { get; private set; }

    public SpriteProvider(LevelData currentLevel)
    {
        Status = Status.Starting;
        _currentLevel = currentLevel;
        Status = Status.Running;
    }

    public List<Sprite> Walls
    {
        get { return _currentLevel.Walls; }
    }
    public List<int> WallsSpawnChance
    {
        get { return _currentLevel.WallsSpawnChance; }
    }
    public List<Sprite> Decoratives
    {
        get { return _currentLevel.Decoratives; }
    }
}
