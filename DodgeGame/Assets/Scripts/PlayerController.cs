using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [SerializeField]private float _moveSpeed;
    private Rigidbody _rigid;
    private float _hMove;
    private float _vMove;

    public event Action OnDied;


    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        InputKey();
        Move();
    }

    private void InputKey()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        _rigid.velocity = new Vector3(_hMove, 0, _vMove) * _moveSpeed;
    }

    public void TakeDamage()
    {
        OnDied?.Invoke();
    }
}
