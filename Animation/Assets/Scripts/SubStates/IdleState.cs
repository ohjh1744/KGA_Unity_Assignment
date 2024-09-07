using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AimState
{
    PlayerStateMachine _player;
    public IdleState(PlayerStateMachine player)
    {
        _player = player;
    }
    public override void Enter()
    {
        Debug.Log("IdleState에 진입");
    }


    public override void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            _player.ChangeAimState(new AimIdleState(_player));
        }
    }

    public override void Exit()
    {
        Debug.Log("IdleState에서 벗어남");
    }
}
