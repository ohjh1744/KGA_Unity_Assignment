using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    public float Speed;

    private void Start()
    {
        rigid.velocity = transform.forward * Speed;

        Destroy(gameObject, 5f);
    }
}
