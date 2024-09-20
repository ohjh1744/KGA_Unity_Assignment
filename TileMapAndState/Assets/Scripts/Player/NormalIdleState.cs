using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class NormalIdleState : NormalState
{
    Player player;
    public NormalIdleState(Player player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Debug.Log("NormalIdleState에 진입");
    }
    public override void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            player.ChangeNormalState(player.NormalStates[(int)ENormalState.Move]);
        }
    }


    public override void Exit()
    {
        Debug.Log("NormalIdleState에서 나감");
    }
}
