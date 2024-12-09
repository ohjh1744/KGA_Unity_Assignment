using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherClass : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TryFire();
        }
    }
    public void TryFire()
    {
        // PhotonView가 부착된 다른 객체를 찾음
        PhotonView otherPhotonView = FindObjectOfType<PhotonView>();

        // 다른 객체가 PhotonView를 가지고 있는 경우에만 RPC 호출
        if (otherPhotonView != null)
        {
            otherPhotonView.RPC("TestFire", RpcTarget.All, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("No PhotonView found!");
        }
    }


}
