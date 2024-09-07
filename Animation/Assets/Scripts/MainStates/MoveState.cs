using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : IState
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
