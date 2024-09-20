using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState : IState
{
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();


}
