using System;
using UnityEngine;

public class SidePlaneEventHandler : MonoBehaviour
{
    public event Action SpawnNewPlaneEvent;
    public event Action<SidePlaneEventHandler> DestroyPlaneEvent;
    public void NextPlaneSpawnPositionReached()
    {
        SpawnNewPlaneEvent?.Invoke();
    }

    public void PlaneDestoryPositionReached()
    {
        DestroyPlaneEvent?.Invoke(this);
    }
}
