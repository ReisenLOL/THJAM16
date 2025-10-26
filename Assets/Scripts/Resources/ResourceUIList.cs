using System.Collections.Generic;
using UnityEngine;

public class ResourceUIList : MonoBehaviour
{
    Dictionary<Resource, ResourceUI> currentResourceUIList = new();
    public ResourceUI templateUI;
    public RectTransform grid;

    public void UpdateResourceUI(Resource resourceToUpdate)
    {
        if (!currentResourceUIList.ContainsKey(resourceToUpdate))
        {
            currentResourceUIList[resourceToUpdate] = CreateNewUI(resourceToUpdate);
        }
        if (currentResourceUIList.TryGetValue(resourceToUpdate, out ResourceUI resourceUI))
        {
            resourceUI.UpdateAmount();  
        }
    }

    public ResourceUI CreateNewUI(Resource resource)
    {
        ResourceUI newResourceUI = Instantiate(templateUI, grid);
        newResourceUI.gameObject.SetActive(true);
        newResourceUI.nameLabel.text = resource.resourceName;
        newResourceUI.icon.color = resource.color;
        newResourceUI.thisResource = resource;
        return newResourceUI;
    }
}
