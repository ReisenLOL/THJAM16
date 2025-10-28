using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestPanelUI : MonoBehaviour
{
    #region Statication
    public static RequestPanelUI instance;
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
    public GameObject requestPanel;
    public GameObject templateResourceIcon;
    public Transform resourceList;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI descriptionLabel;
    public Button acceptButton;
    public TextMeshProUGUI buttonText;

    public void ShowRequest(Request requestToShow, RequestLocation location)
    {
        requestPanel.SetActive(true);
        nameLabel.text = requestToShow.requestName;
        descriptionLabel.text = requestToShow.requestDescription;
        foreach (Transform child in resourceList)
        {
            Destroy(child.gameObject);
        }
        foreach (Request.ResourceQuery resource in requestToShow.resourcesRequested)
        {
            //this is really bad but like i'll make a script later for the resource ui to cache it so i don't have to find all these every single time
            GameObject newResourceIcon = Instantiate(templateResourceIcon, resourceList);
            newResourceIcon.SetActive(true);
            newResourceIcon.transform.Find("Icon").GetComponent<Image>().color = resource.resource.color; 
            newResourceIcon.GetComponentInChildren<TextMeshProUGUI>().text = resource.amount.ToString();
        }
        acceptButton.onClick.RemoveAllListeners();
        if (location.requestInProgress)
        {
            acceptButton.onClick.AddListener(() => DayManager.instance.FulfillRequest(requestToShow));
            buttonText.text = "Complete";
        }
        else
        {
            acceptButton.onClick.AddListener(() => AcceptRequest(requestToShow, location));
            buttonText.text = "Accept";
        }
    }

    public void AcceptRequest(Request request, RequestLocation location)
    {
        requestPanel.SetActive(false);
        DayManager.instance.SetRequestUI();
        DayManager.instance.AcceptRequest(request, location);
        location.requestInProgress = true;
    }
    public void HideRequest()
    {
        requestPanel.SetActive(false);
    }
}
