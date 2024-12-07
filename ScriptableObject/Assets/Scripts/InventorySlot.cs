using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Button button;
    [SerializeField] Image image;

    public void OnClick()
    {
        InventoryManager.Instance.DropItem(index);
    }

    public void SetItem(ItemData item)
    {
        if (item != null)
        {
            button.interactable = true;
            image.sprite = item.icon;
        }
        else
        {
            button.interactable = false;
            image.sprite = null;
        }
    }
}
