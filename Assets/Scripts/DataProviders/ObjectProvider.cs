using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProvider: IObjectProvider
{
    public Dictionary<string, GameObject> Objects { get; private set; }
    public ObjectProvider()
    {
        Objects = new Dictionary<string, GameObject>();
    }

    public GameObject GetObject(string objectName)
    {
        try
        {
            return Objects[objectName];
        }
        catch(NullReferenceException)
        {
            return null;
        }
    }

    public bool AddObject(string objectName, GameObject gameObject)
    {
        try
        {
            Objects.Add(objectName, gameObject);
            return true;
        }
        catch(ArgumentException)
        {
            return false;
        }
    }

    public bool RemoveObject(string objectName)
    {
        try
        {
            Objects.Remove(objectName);
            return true;
        }
        catch(KeyNotFoundException)
        {
            return false;
        }
    }
}
