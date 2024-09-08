using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MoveState
{
    private float _hMove;
    private float _vMove;
    private Vector3 _moveMent;
    private Rigidbody _rigid;
    private Animator _anim;
    PlayerStateMachine _player;
    MoveCamera _moveCamera;
    public WalkState(PlayerStateMachine player)
    {
        _player = player;
    }
    public  override void Enter()
    {
        Debug.Log("WalkState에 진입");
        _rigid = _player.GetComponent<Rigidbody>();
        _anim = _player.GetComponent<Animator>();
        _moveCamera = _player._playerData.Camera;
        _anim.SetBool("isWalk", true);
    }


    public  override void Update()
    {
        Move();
        Turn();
        if (_rigid.velocity == Vector3.zero)
        {
            _player.ChangeMoveState(new WaitState(_player));
        }

    }

    public  override void Exit()
    {
        Debug.Log("WalkState에서 벗어남");
        _anim.SetBool("isWalk", false);
    }
    

    private void Move()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        _moveMent = _moveCamera.PlayerRotation * new Vector3(_hMove, 0, _vMove).normalized;
        _moveMent.y = 0;
        _rigid.velocity = _moveMent * _player._playerData.Speed;
    }

    private void Turn()
    {
        Quaternion turnRate = Quaternion.LookRotation(_moveMent);
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, turnRate, _player._playerData.RotateSpeed * Time.deltaTime);
    }

}
