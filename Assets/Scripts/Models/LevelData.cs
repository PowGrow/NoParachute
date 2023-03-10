using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int LevelId;
    public string LevelName;
    public AudioClip LevelTheme;
    public LevelData NextLevel;
    public LevelData PreviousLevel;
    public List<int> Stars;
    public float FallSpeed;
    public RotationMode RotationMode;
    public float RotationSpeed;
    public int StartObstacleDelay;
    public Color BottomColor;
    public Color BackgroundColor;
    public List<Sprite> Walls;
    public List<Sprite> Decoratives;
    public List<int> WallsSpawnChance;
    public List<int> ObstacleCreateDistance;
    public List<GameObject> ObstacleToCreate;
    public List<float> ObstacleCreateRotation;
    public List<Vector3> TunnelShape;
}
