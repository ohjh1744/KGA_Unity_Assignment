using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] Transform muzzlePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float speed;

    private void Update()
    {

        if(photonView.IsMine == false)
        {
            return;
        }
        //소유권자꺼만 이동할수있도록함.
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3();
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.z = Input.GetAxisRaw("Vertical");

        if(moveDir == Vector3.zero)
        {
            return;
        }

        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = moveDir.normalized;
    }

    private void Fire()
    {
        photonView.RPC("FireRPC", RpcTarget.All, muzzlePoint.position, muzzlePoint.rotation);
    }

    [PunRPC]
    private void FireRPC(Vector3 position, Quaternion rotation)
    {
        Instantiate(bulletPrefab, position, rotation);
    }


}
