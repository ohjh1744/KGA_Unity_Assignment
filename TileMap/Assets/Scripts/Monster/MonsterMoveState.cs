using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Monster;

public class MonsterMoveState : MonsterState
{
    private Monster monster;
    private MonsterData monsterData;
    private GameObject player;
    private float speed;
    public MonsterMoveState(Monster monster)
    {
        this.monster = monster;
        monsterData = this.monster.MonsterData;
        player = monsterData.Player;
        speed = monsterData.Speed;
    }
    public override void Enter()
    {
        Debug.Log("MonsterMove에 진입");
    }

    public override void Update()
    {
        Move();
        if(Vector2.Distance(monster.transform.position, player.transform.position) > 3f)
        {
            monster.ChangeMonsterState(monster.MonsterStates[(int)EMonsterState.Idle]);
        }
        if (Vector2.Distance(monster.transform.position, player.transform.position) < 0.5f)
        {
            monster.ChangeMonsterState(monster.MonsterStates[(int)EMonsterState.Attack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("MonsterMove에 나감");
    }

    private void Move()
    {
        monster.transform.position = Vector2.MoveTowards(monster.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
