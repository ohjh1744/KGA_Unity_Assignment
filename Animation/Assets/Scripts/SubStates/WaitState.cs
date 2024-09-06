using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : MoveState
{
    public override void Enter(PlayerStateMachine player)
    {
        Debug.Log("NotMoveState 진입");
    }

    public override void Update(PlayerStateMachine player)
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            player.ChangeMoveState(new WalkState());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeMoveState(new RollState());
        }
    }

    public override void Exit(PlayerStateMachine player)
    {
        Debug.Log("NotMoveState 나감");
    }
}
