using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : Bullet
{
    [SerializeField] private float _followTime;
    private float _remainTime;
    private  Transform _target;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.Find("Player").transform;
    }
    private void OnDisable()
    {
        _remainTime = 0;
    }
    public override void OnFire()
    {
        _remainTime += Time.deltaTime;
        if(_remainTime > _followTime)
        {
            gameObject.SetActive(false);
        }

        transform.LookAt(_target.position);
        _rigid.velocity = transform.forward * _speed;
    }

}
