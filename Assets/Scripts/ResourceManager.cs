using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    Dictionary<Resource, int> currentResources = new();

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
}
