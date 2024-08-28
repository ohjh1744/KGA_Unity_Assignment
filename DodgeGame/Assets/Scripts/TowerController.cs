using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float bulletTime; //�Ѿ� �����ֱ�
    [SerializeField] private float remainTime; // ���� �Ѿ� �������� ��ٸ� �ð�
    [SerializeField] private GameObject bulletPrefab;

    private bool _isAttacking; 
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //���� �Ѿ� �������� �����ð� ��� ����.
        if(_isAttacking == true)
        {
            remainTime -= Time.deltaTime;

            if (remainTime <= 0)
            {
                GameObject bulletGameObj = Instantiate(bulletPrefab);
                Bullet bullet = bulletGameObj.GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.SetTarget(target);
                remainTime = bulletTime;
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
