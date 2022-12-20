using System.Collections.Generic;
using UnityEngine;

public interface IObstacleProvider
{
    public List<GameObject> Obstacles { get; }
}