using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int LevelId;
    public string LevelName;
    public LevelData NextLevel;
    public float FallSpeed;
    public RotationMode RotationMode;
    public float RotationSpeed;
    public int LevelLength;
    public List<Sprite> Walls;
    public List<Sprite> Decoratives;
    public List<int> WallsSpawnChance;
    public List<GameObject> Obstacles;
    public List<int> ObstacleCreateDistance;
    public List<GameObject> ObstacleToCreate;
    public List<float> ObstacleCreateRotation;
}
