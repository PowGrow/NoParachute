using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Border")
        {
            Time.timeScale = 0;
            other.transform.position = transform.position - new Vector3(0, 0, -0.3f);
        }
    }
}
