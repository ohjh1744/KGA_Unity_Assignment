using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletTime; // �Ѿ� ���� �ֱ�
    [SerializeField] private float _remainTime; // ���� �Ѿ� �������� ��ٸ� �ð�
    [SerializeField] private int _numBulletType; //�Ѿ� Ÿ�� ����

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