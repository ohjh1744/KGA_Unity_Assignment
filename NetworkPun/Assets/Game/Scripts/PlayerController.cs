using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviourPun, IPunObservable
{

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //보낸 개수와 순서는 받는 개수와 순서랑 무조건 일치해야한다.
        // 소유권이 있는 경우
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        // 소유권이 없는 경우
        else if (stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


}
