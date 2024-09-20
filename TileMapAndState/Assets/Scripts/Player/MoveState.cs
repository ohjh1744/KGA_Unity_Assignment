using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class MoveState : NormalState
{
    private Player player;
    private PlayerData playerData;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float hMove;
    private float speed;
    public MoveState(Player player)
    {
        this.player = player;
        this.playerData = player.PlayerData;
        rigid = playerData.Rigid;
        anim = playerData.Anim;
        spriteRenderer = playerData.SpriteRenderer;
        speed = playerData.Speed;
    }

    public override void Enter()
    {
        anim.SetBool("isMove", true);
        Debug.Log("MoveState에 진입");
    }
    public override void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        Move();
        if(rigid.velocity == Vector2.zero)
        {
            player.ChangeNormalState(player.NormalStates[(int)ENormalState.Normalidle]);
        }

    }


    public override void Exit()
    {
        Debug.Log("MoveState에서 나감");
        anim.SetBool("isMove", false);
    }

    private void Move()
    {
        if (hMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (hMove > 0)
        {
            spriteRenderer.flipX = false;
        }
        rigid.velocity = new Vector2(hMove * speed, rigid.velocity.y);
    }
}
