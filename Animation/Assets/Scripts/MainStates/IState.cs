using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void Enter(PlayerStateMachine player);
    void Update(PlayerStateMachine player);
    void Exit(PlayerStateMachine player);
}
