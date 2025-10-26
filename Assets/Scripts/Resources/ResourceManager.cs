using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Statication
    public static ResourceManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    
    Dictionary<Resource, int> currentResources = new();
    public ResourceUIList resourceUI;
    public Resource resourceTest;

    public bool AddResource(Resource resource, int amount)
    {
        int resourceValue = 0;
        if (!currentResources.ContainsKey(resource))
        {
            currentResources[resource] = 0;
        }

        if (currentResources.TryGetValue(resource, out resourceValue))
        {
            resourceValue += amount;
            currentResources[resource] = resourceValue;
            resourceUI.UpdateResourceUI(resource);
            return true;
        }
        return false;
    }
    public bool SubtractResource(Resource resource, int amount)
    {
        int resourceValue = 0;
        if (!currentResources.ContainsKey(resource))
        {
            currentResources[resource] = 0;
        }
        if (currentResources.TryGetValue(resource, out resourceValue))
        {
            resourceValue -= amount;
            currentResources[resource] = resourceValue;
            resourceUI.UpdateResourceUI(resource);
            return true;
        }
        return false;
    }
    public int CheckResourceValue(Resource resource)
    {
        if (!currentResources.ContainsKey(resource))
        {
            return 0;
        }
        return currentResources[resource];
    }

    private void Start()
    {
        AddResource(resourceTest, 90);
    }
}
