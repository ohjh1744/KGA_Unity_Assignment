using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // Model
    [SerializeField] ItemData[] inventory;

    public event UnityAction<int, ItemData> OnInventoryChanged;

    private void Awake()
    {
        inventory = new ItemData[8];

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Controller
    public void AddItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                OnInventoryChanged?.Invoke(i, item);
                break;
            }
        }
    }

    public void DropItem(int index)
    {
        ItemData item = inventory[index];
        inventory[index] = null;

        Instantiate(item.prefab, Vector3.up * 2f, Quaternion.identity);

        OnInventoryChanged?.Invoke(index, null);
    }
}
