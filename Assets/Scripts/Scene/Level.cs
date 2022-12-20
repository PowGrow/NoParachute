using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public int LevelId;
    public int LevelLength;
    public List<Sprite> Walls;
    public List<int> WallsSpawnChance;
    public List<GameObject> Obstacles;
    public List<Sprite> Decoratives;
}
