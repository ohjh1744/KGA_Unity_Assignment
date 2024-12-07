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
        Debug.Log("퀘스트 승낙");

        gatherItem.OnGetItem += Process;
        QuestManager.Instance.AddQuest(this);
    }

    public void Process()
    {
        Debug.Log("퀘스트 진행상황 추가");
        current++;
        if (current >= goal)
        {
            Clear();
        }
    }

    public void Clear()
    {
        // 골드와 경험치 주기
        Debug.Log("퀘스트 클리어!");


        // 클리어 하면 얻는 보상
        foreach (ItemData item in rewardItem)
        {
            InventoryManager.Instance.AddItem(item);
        }

        gatherItem.OnGetItem -= Process;

        QuestManager.Instance.RemoveQuest(this);
    }
}
