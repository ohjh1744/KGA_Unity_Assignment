using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIdleState : AimState
{
    private Animator _anim;
    public override void Enter(PlayerStateMachine player)
    {
        Debug.Log("AimIdleState에 진입");
        _anim = player.GetComponent<Animator>();
        _anim.SetBool("isAim", true);
    }


    public override void Update(PlayerStateMachine player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeAimState(new IdleState());
        }
    }

    public override void Exit(PlayerStateMachine player)
    {
        Debug.Log("AimIdleState에서 벗어남");
        _anim.SetBool("isAim", false);
    }
}
