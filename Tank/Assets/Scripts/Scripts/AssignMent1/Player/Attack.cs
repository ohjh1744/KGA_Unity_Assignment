using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletNum {ù��°źȯ , �ι�°źȯ }
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
            currentBullet = (int)BulletNum.ù��°źȯ;
        }
        else if (_isSecondBullet)
        {
            currentBullet = (int)BulletNum.�ι�°źȯ;
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
