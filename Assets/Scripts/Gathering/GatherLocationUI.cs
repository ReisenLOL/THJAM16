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
    public YieldIcon templateResourceIcon;
    public Transform resourceList;
    public TextMeshProUGUI nameLabel;
    public Button gatherButton;
    public GameObject bonusYieldPanel;
    public Transform bonusResourceList;
    public void ShowGatherLocation(GatherLocation locationToShow)
    {
        gatherPanel.SetActive(true);
        nameLabel.text = locationToShow.locationName;
        foreach (Transform child in resourceList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in bonusResourceList)
        {
            Destroy(child.gameObject);
        }
        foreach (GatherLocation.Yield resource in locationToShow.standardYields)
        {
            YieldIcon newResourceIcon = Instantiate(templateResourceIcon, resourceList);
            newResourceIcon.gameObject.SetActive(true);
            newResourceIcon.icon.color = resource.resource.color;
            newResourceIcon.nameLabel.text = resource.resource.resourceName;
            newResourceIcon.amountLabel.text = resource.amount.ToString();
        }
        gatherButton.onClick.RemoveAllListeners();
        gatherButton.onClick.AddListener(() => Gather(locationToShow));
        if (locationToShow.bonusYields.Length > 0)
        {
            bonusYieldPanel.SetActive(true);
            foreach (GatherLocation.Yield resource in locationToShow.bonusYields)
            {
                YieldIcon newResourceIcon = Instantiate(templateResourceIcon, bonusResourceList);
                newResourceIcon.gameObject.SetActive(true);
                newResourceIcon.icon.color = resource.resource.color;
                newResourceIcon.nameLabel.text = resource.resource.resourceName;
                newResourceIcon.amountLabel.text = resource.amount.ToString();
            }
        }
        else
        {
            bonusYieldPanel.SetActive(false);
        }
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
