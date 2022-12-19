using System.Linq;
using UnityEngine;

public class SpriteChanger : MonoBehaviour, IWallTransformation
{
    [SerializeField] private bool _isActive;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if(IsActive)
        {
            var randomWallId = Random.Range(0, ProjectContext.Instance.SpriteProvider.Walls.Count());
            wall.SpriteRenderer.sprite = ProjectContext.Instance.SpriteProvider.Walls[randomWallId];
        }
    }
}
