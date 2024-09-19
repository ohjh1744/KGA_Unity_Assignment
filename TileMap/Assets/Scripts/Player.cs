using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private float hMove;
    private bool isJump;
    private bool isGround;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private LayerMask layerMask;

    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
        InputKey();
        Idle();
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            gameManager.GameFinish();
        }
    }
    private void InputKey()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        isJump = Input.GetButtonDown("Jump");
    }

    private void Idle()
    {
        if (hMove == 0f)
        {
            anim.SetBool("isMove", false);
        }
    }
    private void Move()
    {
        if (hMove < 0)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("isMove", true);
        }
        else if (hMove > 0)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("isMove", true);
        }
        rigid.velocity = new Vector2(hMove * speed, rigid.velocity.y);
    }

    private void GroundCheck()
    {
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, layerMask);
            Debug.DrawRay(rigid.position, Vector3.down , new Color(0, 1, 0));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                {
                    isGround = true;
                    anim.SetBool("isJump", false);
                }
            }
        }
    }
    private void Jump()
    {
        if (isJump && isGround ==  true)
        {
            anim.SetBool("isJump", true);
            isGround = false;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }



}
