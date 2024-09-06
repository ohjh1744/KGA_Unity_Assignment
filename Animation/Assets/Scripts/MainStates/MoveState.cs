using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : IState
{
    public abstract void Enter(PlayerStateMachine stateMachine);
    public abstract void Update(PlayerStateMachine stateMachine);
    public abstract void Exit(PlayerStateMachine stateMachine);
}
