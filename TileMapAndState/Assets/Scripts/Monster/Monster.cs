using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData;
    private MonsterState currentState;
    public enum EMonsterState {Idle, Move, Attack, Size };
    public MonsterState[] MonsterStates = new MonsterState[(int)EMonsterState.Size];

    private void Awake()
    {
        MonsterStates[(int)EMonsterState.Idle] = new MonsterIdleState(this);
        MonsterStates[(int)EMonsterState.Move] = new MonsterMoveState(this);
        MonsterStates[(int)EMonsterState.Attack] = new MonsterAttackState(this);
    }
    void Start()
    {
        ChangeMonsterState(MonsterStates[(int)EMonsterState.Idle]);

    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Update();
    }

    public void ChangeMonsterState(MonsterState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }


}
