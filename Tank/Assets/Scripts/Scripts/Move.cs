using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
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
        OnTurn();
    }

    void OnMove()
    {
        _movement = new Vector3(_hMove, 0, _vMove);
        _rigid.velocity = _movement * _moveSpeed;
    }

    void OnTurn()
    {
        if(_movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movement.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }

 
}


