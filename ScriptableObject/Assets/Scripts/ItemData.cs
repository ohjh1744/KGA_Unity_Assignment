using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] Command command;
    // 스크립터블 오브젝트의 옵저버 패턴 QuestData쪽에서 함수 연결.
    public event UnityAction OnGetItem;

    public new string name;
    public string description;

    // 인벤토리
    public Sprite icon;

    // 월드
    public GameObject prefab;

    // 상점
    public int cost;

    public void Get()
    {
        OnGetItem?.Invoke();
        InventoryManager.Instance.AddItem(this);
    }

    public void Use()
    {
        command.Excute();
    }
}
