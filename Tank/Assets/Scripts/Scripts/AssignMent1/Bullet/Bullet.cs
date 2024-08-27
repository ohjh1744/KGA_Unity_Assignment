using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    [HideInInspector] public Rigidbody Rigid;
    [SerializeField] private int _bulletDamage;
    public float BulletSpeed;
    private float _remainTime = 10f;
    

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _remainTime -= Time.deltaTime;
        if (_remainTime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            damagable.GetDamage(_bulletDamage);
        }
    }

    private void FixedUpdate()
    {
        OnFired();
    }

    public virtual void OnDisable()
    {
        _remainTime = 5f;
        Rigid.velocity = Vector3.zero;
        Rigid.angularVelocity = Vector3.zero;
    }

    public abstract void OnFired();
}
