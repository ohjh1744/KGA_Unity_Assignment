using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerData _playerData;
    private MoveState _curMoveState;
    private AimState _curAimState;
    private SkillState _curSkillState;
    private void Start()
    {
        ChangeMoveState(new WaitState(this));
        ChangeAimState(new IdleState(this));
        ChangeSkillState(new SkillIdleState(this));
    }

    private void Update()
    {
        _curMoveState?.Update();
        _curAimState?.Update();
        _curSkillState?.Update();
    }

    public void ChangeMoveState(MoveState newState)
    {
        if (_curMoveState != null)
        {
            _curMoveState.Exit();
        }

        _curMoveState = newState;
        _curMoveState.Enter();
    }

    public void ChangeAimState(AimState newState)
    {
        if (_curAimState != null)
        {
            _curAimState.Exit();
        }

        _curAimState = newState;
        _curAimState.Enter();
    }

    public void ChangeSkillState(SkillState newState)
    {
        if (_curSkillState != null)
        {
            _curSkillState.Exit();
        }

        _curSkillState = newState;
        _curSkillState.Enter();
    }

}
