using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetectorForLimbs : MonoBehaviour
{
    
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject limbPrefab;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int maxClashes=12;
    [SerializeField] private ScoreCounter _scoreCounter;
    private int countOfClashes;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Border")
        {
            if(other.tag != "Breakable")
            {
                DestroyLimb();
                GameObject limb = Instantiate(limbPrefab,this.transform.position,quaternion.identity,other.transform);
                limb.transform.position =
                    new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z-0.3f);
            }
        }
        else if (countOfClashes < maxClashes)
            countOfClashes++;
        else
        DestroyLimb();

    }

    private void DestroyLimb()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        _particleSystem.Play();
        GetComponent<BoxCollider>().enabled = false;
        Movement.Instance.DecreseSpeed();
        GetComponent<AudioSource>().Play();
        _scoreCounter.LimbsLostPenalityApply();
    }
    

    
}
