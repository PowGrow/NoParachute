using System.Collections.Generic;
using UnityEngine;

public class SpriteProvider : ISpriteProvider
{
    private LevelData _currentLevel;

    public SpriteProvider(LevelData currentLevel)
    {
        _currentLevel = currentLevel;
    }

    public Color BottomColor
    {
        get { return _currentLevel.BottomColor;}
    }

    public Color BackgroundColor
    {
        get { return _currentLevel.BackgroundColor; }
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

    public void SwitchSpriteData(LevelData levelData)
    {
        _currentLevel = levelData;
    }
}
