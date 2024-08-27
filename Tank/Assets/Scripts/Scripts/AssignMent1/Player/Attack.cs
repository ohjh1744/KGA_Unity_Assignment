using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletNum {첫번째탄환 , 두번째탄환, 세번째탄환 }
public class Attack : MonoBehaviour
{
    private bool _isAttack;
    private bool _isFirstBullet;
    private bool _isSecondBullet;
    private bool _isThirdBullet;
    private int currentBullet;
    [SerializeField] private Transform _posFire;


    // Update is called once per frame
    void Update()
    {
        _isAttack = Input.GetKeyDown(KeyCode.Space);
        _isFirstBullet = Input.GetKeyDown(KeyCode.Alpha1);
        _isSecondBullet = Input.GetKeyDown(KeyCode.Alpha2);
        _isThirdBullet = Input.GetKeyDown(KeyCode.Alpha3);
        ChangeBullet();
        OnAttack();
    }

    void ChangeBullet()
    {
        if (_isFirstBullet)
        {
            currentBullet = (int)BulletNum.첫번째탄환;
        }
        else if (_isSecondBullet)
        {
            currentBullet = (int)BulletNum.두번째탄환;
        }
        else if (_isThirdBullet)
        {
            currentBullet = (int)BulletNum.세번째탄환;
        }
    }

    void OnAttack()
    {
        if (_isAttack)
        {
            GameObject bullet = PullManager._pullManager.GetBullet(currentBullet);
            bullet.transform.position = _posFire.position;
            bullet.transform.rotation = _posFire.rotation;
          
        }
    }
}
