using System;
using System.Collections;
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

    public void Initialize(GameObject wallPrefab, ISpriteProvider spriteProvider, IProgressProvider progressProvider)
    {
        _wallPrefab = wallPrefab;
        _progressProvider = progressProvider;
        if(progressProvider != null)
            progressProvider.SubscribingOnWallCreatingEvents(this);
        this.gameObject.SetActive(true);
    }

    private void CreateWall(GameObject wallPrefab,List<IWallTransformation> wallTransformations)
    {
        var wallObject = Instantiate(wallPrefab, this.transform);
        var wall = wallObject.GetComponent<Wall>();
        foreach(IWallTransformation wallTransformation in wallTransformations)
        {
            wallTransformation.WallTransform(wall);
        }

        wall.EventHandler.CreatingWallEvent += CreatingWallEventHandler;
        wall.EventHandler.DestroyingWallEvent += DestroyWallEventHandler;

        WallCreatedEvent?.Invoke(wall.EventHandler);
    }

    private void CreatingWallEventHandler()
    {
        CreateWall(_wallPrefab,_wallTransformations);
    }
    private void DestroyWallEventHandler(WallEventHandler wallEventHandler)
    {
        WallDestroyingEvent?.Invoke(wallEventHandler);
        wallEventHandler.CreatingWallEvent -= CreatingWallEventHandler;
        wallEventHandler.DestroyingWallEvent -= DestroyWallEventHandler;

        Destroy(wallEventHandler.transform.parent.gameObject);
    }

    private void Awake()
    {
        _wallTransformations = GetComponentsInChildren<IWallTransformation>().ToList();
    }

    private void Start()
    {
        CreateWall(_wallPrefab, _wallTransformations);
    }

    private void OnDestroy()
    {
        if(_progressProvider != null)
            _progressProvider.UnsubscribingFromWallCreatingEvents(this);
    }
}
