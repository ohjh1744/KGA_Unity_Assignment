using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;

    [SerializeField] private float _speed;

    [SerializeField] private float _destoryTime; // 3초로 두기.

    [SerializeField] private Renderer _bulletRenderer;

    public float Speed { get { return _speed; } set { _speed = value; } }

    public float DesstoryTime { get { return _destoryTime; } set { _destoryTime = value; } }

    public Renderer BulletRenderer { get { return _bulletRenderer; } set { _bulletRenderer = value; } }

    private void Start()
    {
        _rigid.velocity = transform.forward * _speed;

        Debug.Log(_destoryTime);

        Destroy(gameObject, _destoryTime );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
