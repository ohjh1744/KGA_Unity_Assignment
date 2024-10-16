using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private FollowCamera _camera;
    private float _hMove;
    private float _vMove;
    private Vector3 _moveVec;
    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        Turn();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _moveVec = _camera._mouseRotation * new Vector3(_hMove, 0, _vMove).normalized;
        _moveVec.y = 0;
        _rigid.velocity = _moveVec * _speed;
    }

    private void Turn()
    {
        if (_moveVec != Vector3.zero)
        {
            Quaternion _playerLookDir = Quaternion.LookRotation(_moveVec);
            transform.rotation = Quaternion.Lerp(transform.rotation, _playerLookDir, _turnSpeed * Time.deltaTime);
        }
    }

}