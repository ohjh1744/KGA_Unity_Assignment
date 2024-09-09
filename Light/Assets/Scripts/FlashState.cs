using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashState : MonoBehaviour
{
    [SerializeField] private GameObject Light;
    [SerializeField] AudioClip _audioClip;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySFX(_audioClip);
            if (Light.activeSelf == true)
            {
                Light.SetActive(false);
            }
            else
            {
                Light.SetActive(true);
            }
        }
    }
}
