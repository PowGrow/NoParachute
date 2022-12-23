using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    [SerializeField] private int _decorativesSpawnChance;

    private ISpriteProvider _spriteProvider;

    public void SelectSpriteFromPool(Wall wall, ISpriteProvider spriteProvider)
    {
        wall.SpriteRenderer.sprite = GetRandomWallSprite(spriteProvider.Walls, spriteProvider.WallsSpawnChance);
        if(spriteProvider.Decoratives.Count() > 0)
            wall.DecorativesSpriteRenderer.sprite = GetRandomDecoratives(spriteProvider.Decoratives, _decorativesSpawnChance);
    }

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

    private void Awake()
    {
        _spriteProvider = ProjectContext.Instance.SceneContext.SpriteProvider;
        var wall = GetComponentInParent<Wall>();
        SelectSpriteFromPool(wall,_spriteProvider);
    }
}
