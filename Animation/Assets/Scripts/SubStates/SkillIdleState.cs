using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIdleState : SkillState
{
    PlayerStateMachine _player;
    public SkillIdleState(PlayerStateMachine player)
    {
        _player = player;
    }
    public override void Enter()
    {
        Debug.Log("SkillIdel���¿� ����!");
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeSkillState(new RollState(_player));
        }
    }
    public override void Exit()
    {
        Debug.Log("SkillIdel���¿� ����!");
    }
}
