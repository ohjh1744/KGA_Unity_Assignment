using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float bulletTime; //총알 생성주기
    [SerializeField] private float remainTime; // 다음 총알 생성까지 기다린 시간
    [SerializeField] private GameObject bulletPrefab;

    private bool _isAttacking; 
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //다음 총알 생성까지 남은시간 계속 차감.
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
