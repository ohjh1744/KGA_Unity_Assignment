using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _bulletPos;

    [SerializeField] private float _bulletSpeed;


    public void FireBullet()
    {
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.rotation = _bulletPos.rotation;
        bullet.transform.position = _bulletPos.position;
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = _bulletPos.transform.forward *_bulletSpeed;
    }
}
