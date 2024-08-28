using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _rigid;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnFire();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
        }
        gameObject.SetActive(false);
    }

    public abstract void OnFire();

}