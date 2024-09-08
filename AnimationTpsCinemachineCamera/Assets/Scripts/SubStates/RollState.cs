using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : SkillState
{
    private Animator _anim;
    private Rigidbody _rigid;
    private float _origintime;
    private float _curTime;

    PlayerStateMachine _player;
    private Vector3 _rollEndPosition;
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
        _rollEndPosition = _player.transform.position + _player.transform.forward * _player._playerData.Speed * 3;
        _anim = _player.GetComponent<Animator>();
        _anim.SetBool("isRoll", true);
  
    }

    public override void Update()
    {
        Roll();
        _curTime -= Time.deltaTime;
        if (_curTime < 0)
        {
            _player.ChangeSkillState(new SkillIdleState(_player));
        }
    }

    public override void Exit()
    {
        _curTime = _origintime;
        _anim.SetBool("isRoll", false);
        Debug.Log("RollState 나감");
    }

    private void Roll()
    {
        Vector3 newPosition = Vector3.Lerp(_player.transform.position, _rollEndPosition, 0.02f);
        _rigid.MovePosition(newPosition);
    }
    
}