using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class RollState : SkillState
{
    private Animator _anim;
    private Rigidbody _rigid;
    private float _origintime;
    private float _curTime;

    PlayerStateMachine _player;
    public RollState(PlayerStateMachine player)
    {
        _player = player;
    }

    public override void Enter()
    {
        Debug.Log("RollState 진입");
        _rigid = _player.GetComponent<Rigidbody>();
        _origintime = _player._playerData.RollTime;
        _curTime = _origintime;
        Roll();
        _anim = _player.GetComponent<Animator>();
        _anim.SetBool("isRoll", true);
    }

    public override void Update()
    {
        _curTime -= Time.deltaTime;
        if (_curTime < 0)
        {
            _player.ChangeSkillState(new SkillIdleState(_player));
        }
    }

    public override void Exit()
    {
        _curTime = _origintime;
        _player._playerData.Speed = _player._playerData.Speed / 3;
        _anim.SetBool("isRoll", false);
        Debug.Log("RollState 나감");
    }

    private void Roll()
    {
        _player._playerData.Speed = _player._playerData.Speed * 3;

    }
    
}