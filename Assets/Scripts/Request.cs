using UnityEngine;

[CreateAssetMenu(fileName = "New Request", menuName = "Scripts/Request")]
public class Request : ScriptableObject
{
    public Material[] materialsRequested;
    public string requestName;
    public string requestDescription;
}
