using System;
using UnityEngine;

public class RequestLocation : Location
{
    public Request request; //there's probably gonna be more than one request at a location, but we'll figure that out
    public bool requestInProgress;
    public void OnMouseDown()
    {
        RequestPanelUI.instance.ShowRequest(request, this);
    }
}
