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
        // PhotonView�� ������ �ٸ� ��ü�� ã��
        PhotonView otherPhotonView = FindObjectOfType<PhotonView>();

        // �ٸ� ��ü�� PhotonView�� ������ �ִ� ��쿡�� RPC ȣ��
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
