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
            wall.SpriteRenderer.sprite = GetRandomWallSprite();
            wall.DecorativesSpriteRenderer.sprite = GetRandomDecoratives();
        }
    }

    private Sprite GetRandomWallSprite()
    {
        var randomWallId = Random.Range(0, ProjectContext.Instance.SpriteProvider.Walls.Count());
        return ProjectContext.Instance.SpriteProvider.Walls[randomWallId];
    }

    private Sprite GetRandomDecoratives()
    {
        if(Random.Range(1,101) <= _decorativesSpawnChance)
        {
            var randomDecorativesId = Random.Range(0, ProjectContext.Instance.SpriteProvider.Decoratives.Count());
            return ProjectContext.Instance.SpriteProvider.Decoratives[randomDecorativesId];
        }
        return null;
    }
}
