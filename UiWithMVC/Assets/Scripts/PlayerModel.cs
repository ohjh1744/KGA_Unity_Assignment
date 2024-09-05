using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private int _bullet;
    [SerializeField] private int _maxBullet;

    public int Bullet { get { return _bullet; }  set { _bullet = value; OnBulletChanged?.Invoke(_bullet); } }
    public UnityAction<int> OnBulletChanged;


    public int MaxBullet { get { return _maxBullet; } set { _maxBullet = value; OnMaxBulletChanged?.Invoke(_maxBullet); } }
    public UnityAction<int> OnMaxBulletChanged;
}
