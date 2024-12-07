using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] Command command;
    // ��ũ���ͺ� ������Ʈ�� ������ ���� QuestData�ʿ��� �Լ� ����.
    public event UnityAction OnGetItem;

    public new string name;
    public string description;

    // �κ��丮
    public Sprite icon;

    // ����
    public GameObject prefab;

    // ����
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
