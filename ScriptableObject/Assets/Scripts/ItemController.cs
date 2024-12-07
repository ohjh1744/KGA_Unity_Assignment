using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ItemData data;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Get();
                break;
            case PointerEventData.InputButton.Right:
                Use();
                break;
        }
    }

    public void Get()
    {
        Destroy(gameObject);
        data.Get();
    }

    public void Use()
    {
        Destroy(gameObject);
        data.Use();
    }

    // ��ü�����Ģ�� �ణ ������, ��ӹ޾Ƽ� override�ϴ¹���� ����. Command���� ��ſ�
    //public virtual void Use()
    //{
    //    Destroy(gameObject);
    //    data.Use();
    //}
}
