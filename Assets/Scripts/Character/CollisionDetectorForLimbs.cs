using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetectorForLimbs : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject limb;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        GameObject huy = Instantiate(limb,this.transform.position,quaternion.identity,other.transform);
        huy.transform.position =
            new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z-0.3f);
        GetComponent<BoxCollider>().enabled = false;
        Movement.Instance.DecreseSpeed();
    }

    
}
