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

    // 객체지향원칙을 약간 버리고, 상속받아서 override하는방법도 있음. Command패턴 대신에
    //public virtual void Use()
    //{
    //    Destroy(gameObject);
    //    data.Use();
    //}
}
