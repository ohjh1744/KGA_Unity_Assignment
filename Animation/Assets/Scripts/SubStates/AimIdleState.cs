using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIdleState : AimState
{
    private Animator _anim;
    PlayerStateMachine _player;
    public AimIdleState(PlayerStateMachine player)
    {
        _player = player;
    }
    public override void Enter()
    {
        Debug.Log("AimIdleState�� ����");
        _anim = _player.GetComponent<Animator>();
        _anim.SetBool("isAim", true);
    }


    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _player.ChangeAimState(new IdleState(_player));
        }
    }

    public override void Exit()
    {
        Debug.Log("AimIdleState���� ���");
        _anim.SetBool("isAim", false);
    }
}
