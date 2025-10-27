using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GatherLocation : MonoBehaviour
{
    [Serializable]
    public class Yield
    {
        public Resource resource;
        public int amount;
    }

    public string locationName;
    public Yield[] standardYields;
    public Yield[] bonusYields;
    public float bonusChance;
    public void Gather()
    {
        if (DayManager.instance.currentDolls > 0)
        {
            DayManager.instance.currentDolls--;
            DayManager.instance.UpdateDollsUI(); // we gotta update this to become it's own thing
            foreach (Yield yield in standardYields)
            {
                ResourceManager.instance.AddResource(yield.resource, yield.amount);
            }

            if (bonusYields.Length > 0)
            {
                float random = Random.Range(0f, 100f);
                if (random <= bonusChance)
                {
                    foreach (Yield yield in bonusYields)
                    {
                        ResourceManager.instance.AddResource(yield.resource, yield.amount);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        GatherLocationUI.instance.ShowGatherLocation(this);
    }
}
