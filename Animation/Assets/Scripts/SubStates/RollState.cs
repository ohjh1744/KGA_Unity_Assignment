using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class RollState : MoveState
{
    private Animator _anim;
    private Rigidbody _rigid;
    private float _origintime;
    private float _curTime;
    private float _rollForce;

    public override void Enter(PlayerStateMachine player)
    {
        Debug.Log("RollState 진입");
        _rigid = player.GetComponent<Rigidbody>();
        _origintime = player._playerData.RollTime;
        _curTime = _origintime;
        _rollForce = player._playerData.RollForce;
        Roll(player);
        _anim = player.GetComponent<Animator>();
        _anim.SetBool("isRoll", true);
    }

    public override void Update(PlayerStateMachine player)
    {
        _curTime -= Time.deltaTime;
        if (_curTime < 0)
        {
            player.ChangeMoveState(new WaitState());
        }
    }

    public override void Exit(PlayerStateMachine player)
    {
        _curTime = _origintime;
        _anim.SetBool("isRoll", false);
        Debug.Log("RollState 나감");
    }

    private void Roll(PlayerStateMachine player)
    {
        //_rigid.AddForce(player.transform.forward * _rollForce);
        _rigid.velocity = player.transform.forward * 3 * player._playerData.Speed;

    }
    
}