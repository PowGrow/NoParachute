using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody2D rb =Movement.Instance.gameObject.GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.AddRelativeForce(direction);
        ParticleSystem _particleSystem = other.GetComponent<ParticleSystem>();
        if(_particleSystem!=null)
            _particleSystem.Play();
        _audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        ParticleSystem _particleSystem = other.GetComponent<ParticleSystem>();
        if(_particleSystem!=null)
            _particleSystem.Play();
    }
}
