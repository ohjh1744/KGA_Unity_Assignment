using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvableBullet : Bullet
{
    private bool _isFired = false;

    public override void OnDisable()
    {
        _isFired = false;
        base.OnDisable();

    }

    public  override void OnFired()
    {
        if(_isFired == false)
        {
            _isFired = true;
            Rigid.AddForce(transform.forward * BulletSpeed);
        }
    }

}
