using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public Resource[] listOfCraftableResources;
    public CraftingButton templateCraftButton;
    public Transform craftingList;
    private void Start()
    {
        foreach (Resource resource in listOfCraftableResources)
        {
            CreateResourceButton(resource);
        }
    }

    public void CraftResource(Resource resourceToCraft)
    {
        foreach (Resource.ResourceAmount ingredient in resourceToCraft.recipe)
        {
            if (ResourceManager.instance.CheckResourceValue(ingredient.resource) < ingredient.amount)
            {
                return;
            }
        }
        foreach (Resource.ResourceAmount ingredient in resourceToCraft.recipe)
        {
            ResourceManager.instance.SubtractResource(ingredient.resource, ingredient.amount);
        }

        ResourceManager.instance.AddResource(resourceToCraft, 1);
    }

    private void CreateResourceButton(Resource resource)
    {
        CraftingButton newCraftingButton = Instantiate(templateCraftButton, craftingList);
        newCraftingButton.gameObject.SetActive(true);
        newCraftingButton.nameLabel.text = resource.resourceName;
        newCraftingButton.icon.color = resource.color;
        foreach (Resource.ResourceAmount ingredient in resource.recipe)
        {
            GameObject newResourceIcon = Instantiate(templateCraftButton.templateRequestResourceIcon, newCraftingButton.requestIconGrid);
            newResourceIcon.SetActive(true);
            newResourceIcon.transform.Find("Icon").GetComponent<Image>().color = ingredient.resource.color; 
            newResourceIcon.GetComponentInChildren<TextMeshProUGUI>().text = ingredient.amount.ToString();
        }
        newCraftingButton.GetComponent<Button>().onClick.AddListener(() => CraftResource(resource));
    }
}
