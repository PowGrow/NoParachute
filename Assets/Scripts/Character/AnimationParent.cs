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
        _polygonForCamera.transform.SetParent(_borderGO);
    }
}
