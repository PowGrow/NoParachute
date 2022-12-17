using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Transform _bottomTransform;
    [SerializeField]private float _rotationStep;

    private int _levelId = 0;
    private Level _currentLevel;
    private float _currentRotation;
    private BorderController _borderController;

    public event Action<WallEventHandler> OnWallCreated;
    public event Action<WallEventHandler> OnWallDestoryed;

    private GameObject CreatePlane()
    {
        var wall = Instantiate(_wallPrefab, this.transform);
        SetWallSprite(wall);
        TryToMirrorWall(wall);
        if (_currentLevel.IsRotated)
            RotateWall(wall);
        var wallEventHandler = wall.GetComponent<WallEventHandler>();
        wallEventHandler.SpawnNewPlaneEvent += SpawnNewPlaneEventHandler;
        wallEventHandler.DestroyPlaneEvent += DestroyPlaneEventHandler;
        OnWallCreated?.Invoke(wallEventHandler);
        return wall;
    }
    private void SetWallSprite(GameObject wall)
    {
        var wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
        var randomWallId = Random.Range(0, _currentLevel.Walls.Count());
        wallSpriteRenderer.sprite = _currentLevel.Walls[randomWallId];
    }
    private void RotateWall(GameObject plane)
    {
        _currentRotation += _rotationStep;
        plane.transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
    }
    private void TryToMirrorWall(GameObject plane)
    {
        var random = Random.Range(0, 100);
        if (random > 50)
            plane.transform.localScale = new Vector2(-1, -1);
    }
    private void SpawnNewPlaneEventHandler()
    {
        CreatePlane();
    }
    private void DestroyPlaneEventHandler(WallEventHandler wallEventHandler)
    {
        OnWallDestoryed?.Invoke(wallEventHandler);
        wallEventHandler.SpawnNewPlaneEvent -= SpawnNewPlaneEventHandler;
        wallEventHandler.DestroyPlaneEvent -= DestroyPlaneEventHandler;

        Destroy(wallEventHandler.gameObject);
    }
    private void Awake()
    {
        _borderController = GetComponentInChildren<BorderController>();
        _currentLevel = Resources.Load<Level>($"ScriptableObjects/Levels/Level_{_levelId}");
    }

    private void Start()
    {
        CreatePlane();
    }
}
