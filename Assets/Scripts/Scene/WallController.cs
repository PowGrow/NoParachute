using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private GameObject _wallPrefab;
    private List<IWallTransformation> _wallTransformations;
    private IProgressProvider _progressProvider;
    private HashSet<Animator> _wallAnimators = new HashSet<Animator>();

    private Vector2 _tunelShapeDelta = Vector2.zero;
    private Vector2 _currentTunelDelta = Vector2.zero;

    public event Action<WallEventHandler> WallCreatedEvent;
    public event Action<WallEventHandler> WallDestroyingEvent;

    public HashSet<Animator> WallAnimators
    {
        get { return _wallAnimators; }
    }

    public void Initialize(GameObject wallPrefab, IProgressProvider progressProvider, WallAnimator wallAnimator)
    {
        _wallPrefab = wallPrefab;
        _progressProvider = progressProvider;
        if(progressProvider != null)
        {
            progressProvider.SubscribingOnWallCreatingEvents(this);

        }
        gameObject.SetActive(true);
    }

    private void CreateWall(GameObject wallPrefab,List<IWallTransformation> wallTransformations)
    {
        var wallObject = Instantiate(wallPrefab, this.transform);
        var wall = wallObject.GetComponent<Wall>();
        if(_progressProvider != null)
        {
            var wallAnimator = wall.Animator;
            wallAnimator.speed = (float)WallAnimator.CurrentSpeed;
            _wallAnimators.Add(wallAnimator);

            if (_progressProvider.DoChangeTunelShape)
            {
                if(_tunelShapeDelta == Vector2.zero)
                    _tunelShapeDelta = TunnelShapeChanger.GetDeltaShape(wall.transform.position);
                _currentTunelDelta += _tunelShapeDelta;
            }
            else
                if (_tunelShapeDelta != Vector2.zero)
                {
                    _progressProvider.TunelShapeId++;
                    _tunelShapeDelta = Vector2.zero;
                }
            wall.transform.position = new Vector3(wall.transform.position.x + _currentTunelDelta.x, wall.transform.position.y + _currentTunelDelta.y, wall.transform.position.z);


        }
        WallCreatedEvent?.Invoke(wall.EventHandler);
        foreach (IWallTransformation wallTransformation in wallTransformations)
        {
            wallTransformation.WallTransform(wall);
        }
        if(_progressProvider != null)
            _progressProvider.SubscribingOnWallPassesEvent(wall.EventHandler);
        wall.EventHandler.CreatingWallEvent += CreatingWallEventHandler;
        wall.EventHandler.DestroyingWallEvent += DestroyWallEventHandler;

    }

    private void CreatingWallEventHandler()
    {
        CreateWall(_wallPrefab,_wallTransformations);
    }
    private void DestroyWallEventHandler(Wall wall)
    {
        if(_progressProvider != null)
            _wallAnimators.Remove(wall.Animator);
        WallDestroyingEvent?.Invoke(wall.EventHandler);
        _progressProvider.UnsubscribingOnWallPassesEvent(wall.EventHandler);
        wall.EventHandler.CreatingWallEvent -= CreatingWallEventHandler;
        wall.EventHandler.DestroyingWallEvent -= DestroyWallEventHandler;

        Destroy(wall.transform.gameObject);
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
