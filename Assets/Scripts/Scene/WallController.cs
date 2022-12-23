using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private GameObject _wallPrefab;
    private List<IWallTransformation> _wallTransformations;
    private IProgressProvider _progressProvider;

    public event Action<WallEventHandler> WallCreatedEvent;
    public event Action<WallEventHandler> WallDestroyingEvent;

    public void Initialize(GameObject wallPrefab, IProgressProvider progressProvider)
    {
        _wallPrefab = wallPrefab;
        _progressProvider = progressProvider;
        this.gameObject.SetActive(true);
    }
    private void CreatePlane(GameObject wallPrefab,List<IWallTransformation> wallTransformations,IProgressProvider progressProvider)
    {
        var wallObject = Instantiate(wallPrefab, this.transform);
        var wall = wallObject.GetComponent<Wall>();
        foreach(IWallTransformation wallTransformation in wallTransformations)
        {
            wallTransformation.WallTransform(wall);
        }
        if(progressProvider != null)
            progressProvider.OnProgress();

        wall.EventHandler.CreateWallEvent += SpawnNewPlaneEventHandler;
        wall.EventHandler.DestroyingWallEvent += DestroyPlaneEventHandler;

        WallCreatedEvent?.Invoke(wall.EventHandler);
    }

    private void SpawnNewPlaneEventHandler()
    {
        CreatePlane(_wallPrefab,_wallTransformations, _progressProvider);
    }
    private void DestroyPlaneEventHandler(WallEventHandler wallEventHandler)
    {
        WallDestroyingEvent?.Invoke(wallEventHandler);
        wallEventHandler.CreateWallEvent -= SpawnNewPlaneEventHandler;
        wallEventHandler.DestroyingWallEvent -= DestroyPlaneEventHandler;

        Destroy(wallEventHandler.transform.parent.gameObject);
    }

    private void Awake()
    {
        _wallTransformations = GetComponentsInChildren<IWallTransformation>().ToList();
    }

    private void Start()
    {
        CreatePlane(_wallPrefab, _wallTransformations, _progressProvider);
    }
}
