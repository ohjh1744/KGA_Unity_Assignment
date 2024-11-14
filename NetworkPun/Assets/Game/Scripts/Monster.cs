using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Monster : MonoBehaviourPun
{
    private void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        Debug.Log("몬스터 동작 진행");
    }
}
