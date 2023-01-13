using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParent : MonoBehaviour
{
    [SerializeField] private GameObject _polygonForCamera;
    public static AnimationParent Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void InitializationCamera(Transform _borderGO)
    {
        Movement.Instance.transform.SetParent(_borderGO);
        Movement.Instance.transform.position = new Vector3(Movement.Instance.transform.position.x,
            Movement.Instance.transform.position.y, _borderGO.transform.position.z);
        _polygonForCamera.transform.SetParent(_borderGO);
    }
}
