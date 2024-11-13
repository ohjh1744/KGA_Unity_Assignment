using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;

    private void Start()
    {
        rigid.velocity = transform.forward * speed;

        Destroy(gameObject, 5f);
    }
}
