using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MonoBehaviour
{
    [SerializeField]private float _speed;
    private Vector3 _moveDir;
    private Rigidbody _rigid;

  
    private void Awake()
    {
        _speed = 5;
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnMove();
    }


    private void OnMove( )
    {
        _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dir = transform.rotation * _moveDir * _speed;
        dir.y = 0f;
        _rigid.velocity = dir;

    }
}
