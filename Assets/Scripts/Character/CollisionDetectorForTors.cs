using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;
        
    }
}
