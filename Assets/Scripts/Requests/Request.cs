using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Request", menuName = "Scripts/Request")]
public class Request : ScriptableObject
{
    [Serializable]
    public class ResourceQuery
    {
        public Resource resource;
        public int amount;
    }
    public ResourceQuery[] resourcesRequested;
    public string requestName;
    public string requestDescription;
}
