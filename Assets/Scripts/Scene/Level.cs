using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public int LevelId;
    public List<Sprite> Walls;
    public List<Sprite> Obstacles;
    public List<Sprite> Decoratives;
}
