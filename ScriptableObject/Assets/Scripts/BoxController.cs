using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ItemData content;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        Instantiate(content.prefab, transform.position, Quaternion.identity);
    }
}
