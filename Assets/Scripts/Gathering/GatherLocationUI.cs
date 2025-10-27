using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatherLocationUI : MonoBehaviour
{
    #region Statication
    public static GatherLocationUI instance;
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
    public GameObject gatherPanel;
    public GameObject templateResourceIcon;
    public Transform resourceList;
    public TextMeshProUGUI nameLabel;
    public Button gatherButton;

    public void ShowGatherLocation(GatherLocation locationToShow)
    {
        gatherPanel.SetActive(true);
        nameLabel.text = locationToShow.locationName;
        foreach (Transform child in resourceList)
        {
            Destroy(child.gameObject);
        }
        foreach (GatherLocation.Yield resource in locationToShow.standardYields)
        {
            //this is really bad but like i'll make a script later for the resource ui to cache it so i don't have to find all these every single time
            GameObject newResourceIcon = Instantiate(templateResourceIcon, resourceList);
            newResourceIcon.SetActive(true);
            newResourceIcon.transform.Find("Icon").GetComponent<Image>().color = resource.resource.color;
            newResourceIcon.transform.Find("NameLabel").GetComponent<TextMeshProUGUI>().text =
                resource.resource.resourceName;
            newResourceIcon.transform.Find("AmountLabel").GetComponent<TextMeshProUGUI>().text = resource.amount.ToString();
        }
        gatherButton.onClick.RemoveAllListeners();
        gatherButton.onClick.AddListener(() => Gather(locationToShow));
    }

    public void Gather(GatherLocation locationToShow)
    {
        //gatherPanel.SetActive(false);
        DayManager.instance.SetResourceUI();
        locationToShow.Gather();
    }
    public void HideUI()
    {
        gatherPanel.SetActive(false);
    }
}
