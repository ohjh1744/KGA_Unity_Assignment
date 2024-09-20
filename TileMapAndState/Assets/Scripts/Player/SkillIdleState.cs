using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class SkillIdleState : SkillState
{
    Player player;
    public SkillIdleState(Player player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Debug.Log("SkillIdleState�� ����");
    }
    public override void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.ChangeSkillState(player.SkillStates[(int)ESKillState.Jump]);
        }
    }


    public override void Exit()
    {
        Debug.Log("SkillIdleState�� ����");
    }
}
