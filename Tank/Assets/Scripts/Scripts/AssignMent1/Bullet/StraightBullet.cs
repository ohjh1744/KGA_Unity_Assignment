using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    public  override void OnFired()
    {
        Rigid.velocity = transform.forward * BulletSpeed;
    }
}
