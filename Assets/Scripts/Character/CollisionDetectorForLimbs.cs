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
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Border")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            GameObject huy = Instantiate(limb,this.transform.position,quaternion.identity,other.transform);
            huy.transform.position =
                new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z-0.1f);
            GetComponent<BoxCollider>().enabled = false;
            
            _particleSystem.Play();
            Movement.Instance.DecreseSpeed();
        }
    }

    
}
