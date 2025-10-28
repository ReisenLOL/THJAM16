using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New Resource", menuName = "Scripts/Resource")]
public class Resource : ScriptableObject
{
    public string resourceName;
    public Sprite icon;
    public int tier; //probably replaced with just a bool for advanced.
    public Color color; //this is a placeholder for the icon
    public ResourceAmount[] recipe; //you can leave blank if none
    [Serializable]
    public class ResourceAmount
    {
        public Resource resource;
        public int amount;
    }
}
