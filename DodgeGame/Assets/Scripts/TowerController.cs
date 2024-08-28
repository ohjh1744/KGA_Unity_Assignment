using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletTime; // 총알 생성 주기
    [SerializeField] private float _remainTime; // 다음 총알 생성까지 기다린 시간
    [SerializeField] private int _numBulletType; //총알 타입 개수

    private bool _isAttacking;

    void Update()
    {
        if (_isAttacking)
        {
            _remainTime -= Time.deltaTime;

            if (_remainTime <= 0)
            {
                int numBullet = Random.Range(0, _numBulletType );
                GameObject bulletGameObj = PullManager._pullManager.GetBullet(numBullet);
                Bullet bullet = bulletGameObj.GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.transform.LookAt(_target.position);

                _remainTime = Random.Range(0, _bulletTime);
            }
        }
    }

    public void StartAttack()
    {
        _isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }
}