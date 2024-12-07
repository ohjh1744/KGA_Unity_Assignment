using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAccepter : MonoBehaviour
{
    [SerializeField] QuestData quest;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            QuestData questInstance = Instantiate(quest);
            questInstance.Accept();
        }
    }
}
