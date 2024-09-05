using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MonsterModel : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;

    public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(_hp, _maxHp); } }
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public UnityAction<float, float> OnHpChanged;

}
