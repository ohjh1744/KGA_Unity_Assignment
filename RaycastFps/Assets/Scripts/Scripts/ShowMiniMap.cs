using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMiniMap : MonoBehaviour
{
    [SerializeField] private GameObject _miniMap;

    private void Update()
    {
        ShowMap();
    }
    void ShowMap()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _miniMap.SetActive(true);
        }
        else
        {
            _miniMap.SetActive(false);
        }
    }
}
