using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Resource thisResource;
    public Image icon;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI amountLabel;

    public void UpdateAmount()
    {
        amountLabel.text = ResourceManager.instance.CheckResourceValue(thisResource).ToString();
    }
}
