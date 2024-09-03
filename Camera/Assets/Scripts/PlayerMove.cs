using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _moveSpeed;
    private Rigidbody _rigid;
    private float _hMove;
    private float _vMove;
    private Vector3 _movement;
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");

        OnMove();
    }

    void OnMove()
    {
        Vector3 verticalMove = _vMove * transform.forward;
        Vector3 horizontalMove = _hMove * transform.right;
        _movement = (verticalMove + horizontalMove).normalized;
        _movement.y = 0;
        _rigid.velocity = _movement * _moveSpeed;
    }

}
