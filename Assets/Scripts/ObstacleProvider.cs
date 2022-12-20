using System.Collections.Generic;
using UnityEngine;

public class ObstacleProvider : IObstacleProvider
{
    private LevelData _currentLevel;

    public ObstacleProvider(LevelData currentLevel)
    {
        _currentLevel = currentLevel;
    }

    public List<int> ObstacleCreateDistance
    {
        get { return _currentLevel.ObstacleCreateDistance; }
    }

    public List<GameObject> ObstacleToCreate
    {
        get { return _currentLevel.ObstacleToCreate; }
    }
    public List<float> ObstacleCreateRotation
    {
        get { return _currentLevel.ObstacleCreateRotation; }
    }
}
