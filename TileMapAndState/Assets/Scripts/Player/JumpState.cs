using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpState : SkillState
{
    private Player player;
    private PlayerData playerData;
    private Rigidbody2D rigid;
    private Animator anim;
    private LayerMask layerMask = LayerMask.GetMask("Floor");
    private float jumpPower;
    private bool isGround;
    public JumpState(Player player)
    {
        this.player = player;
        this.playerData = player.PlayerData;
        rigid = playerData.Rigid;
        anim = playerData.Anim;
        jumpPower = playerData.JumpPower;
    }

    public override void Enter()
    {
        Debug.Log("JumpState에 진입");
        GroundCheck();
        Jump();
    }
    public override void Update()
    {
        GroundCheck();
        if (isGround == true)
        {
            player.ChangeSkillState(player.SkillStates[(int)ESKillState.SkillIdle]);
        }
    }


    public override void Exit()
    {
        Debug.Log("JumpState에서 나감");
        anim.SetBool("isJump", false);
    }


    private void GroundCheck()
    {
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, layerMask);
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                {
                    isGround = true;
                }
            }
        }
    }
    private void Jump()
    {
        if (isGround == true)
        {
            anim.SetBool("isJump", true);
            isGround = false;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
