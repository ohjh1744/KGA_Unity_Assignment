using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class MonsterAttackState : MonsterState
{
    private Monster monster;
    private MonsterData monsterData;
    private int damage;
    private GameObject player;
    private IDamagable damagable;
    public MonsterAttackState(Monster monster)
    {
        this.monster = monster;
        monsterData = this.monster.MonsterData;
        damage = monsterData.Damage;
        player = monsterData.Player;
        damagable = player.GetComponent<IDamagable>();
    }
    public override void Enter()
    {
        Debug.Log("MonsterAttack에 진입");
        Attack();
    }

    public override void Update()
    {
        monster.ChangeMonsterState(monster.MonsterStates[(int)EMonsterState.Idle]);
    }

    public override void Exit()
    {
        Debug.Log("MonsterAttack에 나감");
    }

    private void Attack()
    {
        damagable.TakeDamage(damage);
    }
}
