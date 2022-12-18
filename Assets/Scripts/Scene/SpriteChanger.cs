using System.Linq;
using UnityEngine;

public class SpriteChanger : MonoBehaviour, IWallTransformation
{
    [SerializeField] private bool _isActive;

    private int _levelId = 0; //TO-DO: levelInformationHandler
    private Level _currentLevel;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if(IsActive)
        {
            var randomWallId = Random.Range(0, _currentLevel.Walls.Count());
            wall.SpriteRenderer.sprite = _currentLevel.Walls[randomWallId];
        }
    }

    private void Awake()
    {
        _currentLevel = Resources.Load<Level>($"ScriptableObjects/Levels/Level_{_levelId}");
    }
}
