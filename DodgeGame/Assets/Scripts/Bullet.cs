using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] Transform _target;
    Rigidbody _rigid;
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.LookAt(_target.position);
    }

    // Update is called once per frame
    void Update()
    {
        _rigid.velocity = transform.forward * _speed;
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
