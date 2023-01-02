using System.Collections.Generic;
using UnityEngine;

public interface IObjectProvider
{
    public Dictionary<string,GameObject> Objects { get; }

    public GameObject GetObject(string objectName);

    public bool AddObject(string objectName, GameObject gameObject);
    public bool RemoveObject(string objectName);
}
