using System;
using UnityEngine;

public class WallEventHandler : MonoBehaviour
{
    [SerializeField] private Wall _wall;
    public event Action CreatingWallEvent;
    public event Action<Wall> DestroyingWallEvent;
    public event Action<Quaternion,Vector3> RefreshBorderEvent;
    public event Action LevelProgressEvent;
    public event Action ObstacleFadeOutEvent;

    public void RefreshBorderPosition()
    {
        LevelProgressEvent?.Invoke();
        RefreshBorderEvent?.Invoke(this.transform.rotation, this.transform.position);
    }
    public void WallCreatePositionReached()
    {
        CreatingWallEvent?.Invoke();
    }

    public void WallDestroyPositionReached()
    {
        DestroyingWallEvent?.Invoke(_wall);
    }

    public void ObstacleFadeOutPositionReached()
    {
        ObstacleFadeOutEvent?.Invoke();
    }
}
