using System;
using UnityEngine;

public class WallEventHandler : MonoBehaviour
{
    public event Action SpawnNewPlaneEvent;
    public event Action<WallEventHandler> DestroyPlaneEvent;
    public event Action<Quaternion,Vector3> RefreshBorderEvent;
    public event Action LevelProgressEvent;

    public void RefreshBorderPosition()
    {
        LevelProgressEvent?.Invoke();
        RefreshBorderEvent?.Invoke(this.transform.rotation, this.transform.position);
    }
    public void NextPlaneSpawnPositionReached()
    {
        SpawnNewPlaneEvent?.Invoke();
    }

    public void PlaneDestoryPositionReached()
    {
        DestroyPlaneEvent?.Invoke(this);
    }
}
