using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// this script will be split up as necessary but for now it'll just be one
public class DayManager : MonoBehaviour
{
    #region Statication
    public static DayManager instance;
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
    
    public List<Request> currentRequests = new();
    public int currentDay;
    public enum Phase {Requesting, Gathering, Crafting}

    public Phase currentPhase;
    public int maxDolls;
    public int currentDolls;

    public GameObject requestLocations;
    public GameObject gatherLocations;
    
    [Header("UI")]
    public RequestUIList requestUIList;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI dollAmountText;
    public GameObject requestList;
    public GameObject resourceList;
    public GameObject craftingList;

    public void NextPhase()
    {
        currentPhase++;
        if (currentPhase > Phase.Crafting)
        {
            currentPhase = Phase.Requesting;
            EndDay();
        }
        switch (currentPhase)
        {
            case Phase.Requesting:
            {
                requestLocations.SetActive(true);
                gatherLocations.SetActive(false);
                break;
            }
            case Phase.Gathering:
            {
                requestLocations.SetActive(false);
                gatherLocations.SetActive(true);
                break;
            }
        }
        phaseText.text = currentPhase.ToString();
    }

    private void EndDay()
    {
        currentDay++;
        currentDolls = maxDolls;
    }
    public void UpdateDollsUI()
    {
        dollAmountText.text = $"Dolls: {currentDolls}/{maxDolls}";
    }
    public void AcceptRequest(Request request)
    {
        currentRequests.Add(request);
        requestUIList.CreateNewUI(request);
    }

    public void SetResourceUI()
    {
        resourceList.SetActive(true);
        requestList.SetActive(false);
        craftingList.SetActive(false);
    }
    public void SetRequestUI() //this is stupid to have these be their own functions there's gotta be a better way - sylvia
    {
        resourceList.SetActive(false);
        requestList.SetActive(true);
        craftingList.SetActive(false);
    }

    public void SetCraftingUI()
    {
        resourceList.SetActive(false);
        requestList.SetActive(false);
        craftingList.SetActive(true);
    }
}
