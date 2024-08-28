using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hp;


    private void Update()
    {
        if(_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _hp = 3;
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
    }

}
