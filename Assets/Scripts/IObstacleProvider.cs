using System.Collections.Generic;
using UnityEngine;

public interface IObstacleProvider
{
    public List<int> ObstacleCreateDistance { get; }
    public List<GameObject> ObstacleToCreate { get; }
    public List<float> ObstacleCreateRotation { get; }
}