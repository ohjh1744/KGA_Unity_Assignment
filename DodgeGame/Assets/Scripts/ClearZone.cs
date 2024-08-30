using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Save();
            gameManager.Load();
        }
    }
}
