using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : MoveState
{
    PlayerStateMachine _player;
    public WaitState(PlayerStateMachine player)
    {
        _player = player;
    }
    public override void Enter()
    {
        Debug.Log("NotMoveState 진입");
    }

    public override void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _player.ChangeMoveState(new WalkState(_player));
        }
    }

    public override void Exit()
    {
        Debug.Log("NotMoveState 나감");
    }
}
