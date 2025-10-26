using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestUIList : MonoBehaviour
{
    Dictionary<Request, RequestIconUI> currentRequestList = new();
    public RequestIconUI templateUI;
    public RectTransform grid;

    public void CreateNewUI(Request request)
    {
        RequestIconUI newRequestIcon = Instantiate(templateUI, grid);
        newRequestIcon.gameObject.SetActive(true);
        newRequestIcon.gameObject.SetActive(true);
        newRequestIcon.GetComponentInChildren<TextMeshProUGUI>().text = request.requestName;
        foreach (Request.ResourceQuery resource in request.resourcesRequested)
        {
            GameObject newResourceUI =
                Instantiate(templateUI.templateRequestResourceIcon, newRequestIcon.requestIconGrid);
            newResourceUI.SetActive(true);
            newResourceUI.GetComponentInChildren<TextMeshProUGUI>().text = resource.amount.ToString();
            newResourceUI.GetComponentInChildren<Image>().color = resource.resource.color;
        }
    }

    public void RemoveIcon(Request request)
    {
        if (currentRequestList.TryGetValue(request, out RequestIconUI requestIconUI))
        {
            Destroy(requestIconUI.gameObject);
        }
    }
}
