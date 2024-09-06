using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerData _playerData;
    private MoveState _curMoveState;
    private AimState _curAimState;
    private void Start()
    {
        ChangeMoveState(new WaitState());
        ChangeAimState(new IdleState());
    }

    private void Update()
    {
        _curMoveState?.Update(this);
        _curAimState?.Update(this);
    }

    public void ChangeMoveState(MoveState newState)
    {
        if (_curMoveState != null)
        {
            _curMoveState.Exit(this);
        }

        _curMoveState = newState;
        _curMoveState.Enter(this);
    }

    public void ChangeAimState(AimState newState)
    {
        if (_curAimState != null)
        {
            _curAimState.Exit(this);
        }

        _curAimState = newState;
        _curAimState.Enter(this);
    }

}
