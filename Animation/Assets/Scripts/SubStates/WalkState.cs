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

    public  override void Enter(PlayerStateMachine player)
    {
        Debug.Log("WalkState에 진입");
        _rigid = player.GetComponent<Rigidbody>();
        _anim = player.GetComponent<Animator>();
        _anim.SetBool("isWalk", true);
    }


    public  override void Update(PlayerStateMachine player)
    {
        Move(player);
        Turn(player);
        if(_rigid.velocity == Vector3.zero)
        {
            player.ChangeMoveState(new WaitState());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeMoveState(new RollState());
        }

    }

    public  override void Exit(PlayerStateMachine player)
    {
        Debug.Log("WalkState에서 벗어남");
        _anim.SetBool("isWalk", false);
    }
    

    private void Move(PlayerStateMachine player)
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        _moveMent = new Vector3(_hMove, 0, _vMove).normalized;
        _rigid.velocity = _moveMent * player._playerData.Speed;
    }

    private void Turn(PlayerStateMachine player)
    {
        Quaternion turnRate = Quaternion.LookRotation(_moveMent);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, turnRate, player._playerData.RotateSpeed * Time.deltaTime);
    }

}
