using System.Collections.Generic;
using UnityEngine;

public class ObstacleProvider : IObstacleProvider
{
    private Level _currentLevel;

    public ObstacleProvider(Level currentLevel)
    {
        _currentLevel = currentLevel;
    }

    public List<GameObject> Obstacles
    {
        get { return _currentLevel.Obstacles; }
    }
}
