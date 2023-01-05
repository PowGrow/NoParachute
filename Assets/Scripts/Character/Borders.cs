using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private Vector2 direction;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody2D rb =Movement.Instance.gameObject.GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.AddRelativeForce(direction);
    }
}
