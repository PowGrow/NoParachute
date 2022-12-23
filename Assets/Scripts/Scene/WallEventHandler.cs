using System;
using UnityEngine;

public class WallEventHandler : MonoBehaviour
{
    public event Action CreateWallEvent;
    public event Action<WallEventHandler> DestroyingWallEvent;
    public event Action<Quaternion,Vector3> RefreshBorderEvent;
    public event Action LevelProgressEvent;

    public void RefreshBorderPosition()
    {
        LevelProgressEvent?.Invoke();
        RefreshBorderEvent?.Invoke(this.transform.rotation, this.transform.position);
    }
    public void WallCreatePositionReached()
    {
        CreateWallEvent?.Invoke();
    }

    public void WallDestroyPositionReached()
    {
        DestroyingWallEvent?.Invoke(this);
    }
}
