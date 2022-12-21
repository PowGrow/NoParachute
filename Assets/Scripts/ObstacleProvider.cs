using System.Collections.Generic;
using UnityEngine;

public class ObstacleProvider : IObstacleProvider, IProvider
{
    private LevelData _currentLevel;

    public Status Status { get; private set; }

    public ObstacleProvider(LevelData currentLevel)
    {
        Status = Status.Starting;
        _currentLevel = currentLevel;
        Status = Status.Running;
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
