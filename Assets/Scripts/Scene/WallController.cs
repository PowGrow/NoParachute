using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Transform _bottomTransform;

    private List<IWallTransformation> _wallTransformations;

    public event Action<WallEventHandler> OnWallCreated;
    public event Action<WallEventHandler> OnWallDestoryed;

    private void CreatePlane()
    {
        var wallObject = Instantiate(_wallPrefab, this.transform);
        var wall = wallObject.GetComponent<Wall>();
        foreach(IWallTransformation wallTransformation in _wallTransformations)
        {
            wallTransformation.WallTransform(wall);
        }

        wall.EventHandler.SpawnNewPlaneEvent += SpawnNewPlaneEventHandler;
        wall.EventHandler.DestroyPlaneEvent += DestroyPlaneEventHandler;
        OnWallCreated?.Invoke(wall.EventHandler);
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

        Destroy(wallEventHandler.transform.parent.gameObject);
    }
    private void Awake()
    {
        _wallTransformations = GetComponentsInChildren<IWallTransformation>().ToList();
    }

    private void Start()
    {
        CreatePlane();
    }

}
