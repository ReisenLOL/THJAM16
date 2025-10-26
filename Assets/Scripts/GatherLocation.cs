using UnityEngine;

public class GatherLocation : MonoBehaviour
{
    public class Yield
    {
        public Resource resource;
        public int amount;
    }
    public Yield[] standardYields;
    public Yield[] bonusYields;
    public float bonusChance;
    public void Gather()
    {
        foreach (Yield yield in standardYields)
        {
            ResourceManager.instance.AddResource(yield.resource, yield.amount);
        }
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
