using System;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;
    public event Action PlayerDeathEvent;
    private void OnTriggerEnter(Collider other)
    {
        IsAlive = false;
        PlayerDeathEvent?.Invoke();
    }
}
