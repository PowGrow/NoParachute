using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteChanger : MonoBehaviour, IWallTransformation
{
    [SerializeField] private bool _isActive;
    [SerializeField] private int _decorativesSpawnChance;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if(IsActive)
        {
            wall.SpriteRenderer.sprite = GetRandomWallSprite(ProjectContext.Instance.SpriteProvider.Walls, ProjectContext.Instance.SpriteProvider.WallsSpawnChance);
            wall.DecorativesSpriteRenderer.sprite = GetRandomDecoratives(ProjectContext.Instance.SpriteProvider.Decoratives, _decorativesSpawnChance);
        }
    }

    //private Sprite GetRandomWallSprite(List<Sprite> wallsSpriteList)
    //{
    //    var randomWallId = Random.Range(0, wallsSpriteList.Count());
    //    return wallsSpriteList[randomWallId];
    //}

    private Sprite GetRandomWallSprite(List<Sprite> wallsSpriteList, List<int> wallsSpawnChanceList)
    {
        var summChance = wallsSpawnChanceList.Sum();
        var randomValue = Random.Range(1,summChance + 1);
        var counter = 0;
        for(int i=0; i < wallsSpawnChanceList.Count(); i++)
        {
            counter += wallsSpawnChanceList[i];
            if (randomValue <= counter)
                return wallsSpriteList[i];
        }
        Debug.LogError("WallsSpawnChanceList not full");
        return null;
    }

    private Sprite GetRandomDecoratives(List<Sprite> decorativesSritelist,int spawnChance)
    {
        if(Random.Range(1,101) <= spawnChance)
        {
            var randomDecorativesId = Random.Range(0, decorativesSritelist.Count());
            return decorativesSritelist[randomDecorativesId];
        }
        return null;
    }
}
