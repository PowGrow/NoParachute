using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private GameObject _wallPrefab;
    private List<IWallTransformation> _wallTransformations;
    private IProgressHandler _progressHandler;

    public event Action<WallEventHandler> OnWallCreated;
    public event Action<WallEventHandler> OnWallDestoryed;

    public void Initialize(GameObject wallPrefab, IProgressHandler progressHandler)
    {
        _wallPrefab = wallPrefab;
        _progressHandler = progressHandler;
        this.gameObject.SetActive(true);
    }
    private void CreatePlane(GameObject wallPrefab,List<IWallTransformation> wallTransformations,IProgressHandler progressHandler)
    {
        var wallObject = Instantiate(wallPrefab, this.transform);
        var wall = wallObject.GetComponent<Wall>();
        foreach(IWallTransformation wallTransformation in wallTransformations)
        {
            wallTransformation.WallTransform(wall);
        }
        progressHandler.OnProgress();

        wall.EventHandler.SpawnNewPlaneEvent += SpawnNewPlaneEventHandler;
        wall.EventHandler.DestroyPlaneEvent += DestroyPlaneEventHandler;

        OnWallCreated?.Invoke(wall.EventHandler);
    }

    private void SpawnNewPlaneEventHandler()
    {
        CreatePlane(_wallPrefab,_wallTransformations, _progressHandler);
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
        CreatePlane(_wallPrefab, _wallTransformations, _progressHandler);
    }
}
