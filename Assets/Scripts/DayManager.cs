using System;
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

    [Serializable]
    public class RequestCounter
    {
        public Request request;
        public RequestLocation location;
        public int currentDay = 0;
    }
    public List<RequestCounter> currentRequests = new();
    [Header("Game State")]
    public int currentDay;
    public enum Phase {Requesting, Gathering, Crafting}
    public Phase currentPhase;
    [Header("Dolls")]
    public int maxDolls;
    public int currentDolls;
    [Header("Locations")] 
    public Location[] allLocations;
    public GameObject requestLocationFolder;
    public GameObject gatherLocationFolder;
    
    [Header("UI")]
    public RequestUIList requestUIList;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI dollAmountText;
    public GameObject requestList;
    public GameObject resourceList;
    public GameObject craftingList;
    public GameObject[] UIToHide;

    public void NextPhase()
    {
        foreach (GameObject ui in UIToHide)
        {
            ui.SetActive(false);
        }
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
                requestLocationFolder.SetActive(true);
                gatherLocationFolder.SetActive(false);
                break;
            }
            case Phase.Gathering:
            {
                requestLocationFolder.SetActive(false);
                gatherLocationFolder.SetActive(true);
                break;
            }
            case Phase.Crafting:
            {
                requestLocationFolder.SetActive(false);
                gatherLocationFolder.SetActive(false);
                break;
            }
        }
        phaseText.text = currentPhase.ToString();
    }

    private void EndDay()
    {
        currentDay++;
        currentDolls = maxDolls;
        foreach (RequestCounter request in currentRequests.ToArray())
        {
            request.currentDay++;
            if (request.currentDay >= request.request.dayLimit)
            {
                FailRequest(request);
            }
        }

        foreach (Location location in allLocations)
        {
            if (currentDay > location.dayUnlocked)
            {
                location.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateDollsUI()
    {
        dollAmountText.text = $"Dolls: {currentDolls}/{maxDolls}";
    }
    public void AcceptRequest(Request request, RequestLocation location)
    {
        RequestCounter newRequest = new RequestCounter();
        newRequest.request = request;
        newRequest.location = location;
        currentRequests.Add(newRequest);
        requestUIList.CreateNewUI(request);
    }
    public void FulfillRequest(Request requestToFulfill)
    {
        foreach (Request.ResourceQuery query in requestToFulfill.resourcesRequested)
        {
            if (ResourceManager.instance.CheckResourceValue(query.resource) < query.amount)
            {
                return;
            }
        }
        foreach (Request.ResourceQuery query in requestToFulfill.resourcesRequested)
        {
            ResourceManager.instance.SubtractResource(query.resource, query.amount);
        }

        foreach (RequestCounter request in currentRequests.ToArray())
        {
            if (request.request == requestToFulfill)
            {
                request.location.requestInProgress = false;
                currentRequests.Remove(request); //five million for loops
            }
        }
    }

    public void FailRequest(RequestCounter requestToFail)
    {
        requestUIList.RemoveIcon(requestToFail.request);
        requestToFail.location.requestInProgress = false;
        currentRequests.Remove(requestToFail);
        
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
    public void HideAllUI()
    {
        resourceList.SetActive(false);
        requestList.SetActive(false);
        craftingList.SetActive(false);
    }
}
