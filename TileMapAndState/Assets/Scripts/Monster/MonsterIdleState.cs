using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class MonsterIdleState : MonsterState
{
    private Monster monster;
    private MonsterData monsterData;
    private GameObject player;
    private Vector2 startPos;
    private float speed;
    public MonsterIdleState(Monster monster)
    {
        this.monster = monster;
        monsterData = this.monster.MonsterData;
        player = monsterData.Player;
        startPos = monsterData.StartPos;
        speed = monsterData.Speed;

    }
    public override void Enter()
    {
        Debug.Log("MonsterIdleState에진입");
    }

    public override void Update()
    {
        ReMove();
        if (Vector2.Distance(monster.transform.position, player.transform.position) < 3f)
        {
            monster.ChangeMonsterState(monster.MonsterStates[(int)EMonsterState.Move]);
        }
    }

    public override void Exit()
    {
         Debug.Log("MonsterIdleState에서 나감");
    }


    private void ReMove()
    {
        monster.transform.position = Vector2.MoveTowards(monster.transform.position, startPos, speed * Time.deltaTime);
    }
}
