using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviourPun, IPunObservable
{

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //���� ������ ������ �޴� ������ ������ ������ ��ġ�ؾ��Ѵ�.
        // �������� �ִ� ���
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        // �������� ���� ���
        else if (stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


}
