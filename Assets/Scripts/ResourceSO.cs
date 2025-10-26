using UnityEngine;
[CreateAssetMenu(fileName = "New Resource", menuName = "Scripts/Resource")]
public class Resource : ScriptableObject
{
    public string resourceName;
    public Sprite icon;
    public float tier;
    public Color color; //this is a placeholder for the icon
}
