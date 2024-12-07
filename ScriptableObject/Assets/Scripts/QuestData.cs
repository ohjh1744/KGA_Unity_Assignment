using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Quest")]
public class QuestData : ScriptableObject
{
    public new string name;
    public string description;

    public ItemData gatherItem;
    public int current;
    public int goal;

    public int rewardGold;
    public int rewardExp;
    public ItemData[] rewardItem;

    public void Accept()
    {
        Debug.Log("����Ʈ �³�");

        gatherItem.OnGetItem += Process;
        QuestManager.Instance.AddQuest(this);
    }

    public void Process()
    {
        Debug.Log("����Ʈ �����Ȳ �߰�");
        current++;
        if (current >= goal)
        {
            Clear();
        }
    }

    public void Clear()
    {
        // ���� ����ġ �ֱ�
        Debug.Log("����Ʈ Ŭ����!");


        // Ŭ���� �ϸ� ��� ����
        foreach (ItemData item in rewardItem)
        {
            InventoryManager.Instance.AddItem(item);
        }

        gatherItem.OnGetItem -= Process;

        QuestManager.Instance.RemoveQuest(this);
    }
}
