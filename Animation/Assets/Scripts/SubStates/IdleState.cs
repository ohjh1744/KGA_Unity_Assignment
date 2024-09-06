using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AimState
{
    public override void Enter(PlayerStateMachine player)
    {
        Debug.Log("IdleState�� ����");
    }


    public override void Update(PlayerStateMachine player)
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeAimState(new AimIdleState());
        }
    }

    public override void Exit(PlayerStateMachine player)
    {
        Debug.Log("IdleState���� ���");
    }
}
