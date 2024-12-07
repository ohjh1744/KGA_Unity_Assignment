using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] List<QuestData> quests = new List<QuestData>();

    private void Awake()
    {
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

    public void AddQuest(QuestData quest)
    {
        quests.Add(quest);
    }

    public void RemoveQuest(QuestData quest)
    {
        quests.Remove(quest);
    }
}
