using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoom : MonoBehaviour
{
    [SerializeField] GameObject ZoomUi;


    // Update is called once per frame
    void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomUi.SetActive(true);
        }
        else
        {
            ZoomUi.SetActive(false);
        }
    }
}
