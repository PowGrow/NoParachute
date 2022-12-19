using System.Collections.Generic;
using UnityEngine;

public class SpriteProvider : ISpriteProvider
{
    private Level _currentLevel;

    public int LevelId
    {
        get { return _currentLevel.LevelId; }
    }
    public List<Sprite> Walls
    {
        get { return _currentLevel.Walls; }
    }
    public List<int> WallsSpawnChance
    {
        get { return _currentLevel.WallsSpawnChance; }
    }
    public List<Sprite> Obstacles
    {
        get { return _currentLevel.Obstacles; }
    }
    public List<Sprite> Decoratives
    {
        get { return _currentLevel.Decoratives; }
    }

    public SpriteProvider(Level currentLevel)
    {
        _currentLevel = currentLevel;
    }
}
