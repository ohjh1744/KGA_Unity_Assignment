using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviourPun
{
    [PunRPC]
    public void TestFire(Vector3 position, Quaternion rotation)
    {
        // �Ѿ� �߻� ó��
        Debug.Log("Fire called at position: " + position);
    }
}
