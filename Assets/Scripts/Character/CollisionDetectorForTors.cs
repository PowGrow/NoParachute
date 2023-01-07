using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    private AudioSource _audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Border")
        {
            Movement.Instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z-0.2f);
            _audioSource.Play();
            Time.timeScale = 0;
            
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
