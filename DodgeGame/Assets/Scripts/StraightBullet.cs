using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    public override void OnFire()
    {
        if (gameObject.activeSelf)
        {
            _rigid.velocity = transform.forward * _speed;
        }
    }
}
