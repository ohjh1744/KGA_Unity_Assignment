using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField]private GameManager gameManager;
    public PlayerData PlayerData;
    private NormalState currentNormalState;
    private SkillState currentSkillState;

    public enum ENormalState {Normalidle, Move, Size};
    public enum ESKillState { SkillIdle, Jump, Size};
    public NormalState[] NormalStates = new NormalState[(int)ENormalState.Size];
    public SkillState[] SkillStates = new SkillState[(int)ESKillState.Size];


    private void Awake()
    {
        NormalStates[(int)ENormalState.Normalidle] = new NormalIdleState(this);
        NormalStates[(int)ENormalState.Move] = new MoveState(this);
        SkillStates[(int)ESKillState.SkillIdle] = new SkillIdleState(this);
        SkillStates[(int)ESKillState.Jump] = new JumpState(this);
    }

    private void Start()
    {
        ChangeNormalState(NormalStates[(int)ENormalState.Normalidle]);
        ChangeSkillState(SkillStates[(int)ESKillState.SkillIdle]);
    }

    private void Update()
    {
        currentNormalState?.Update();
        currentSkillState?.Update();

        if(PlayerData.Hp <= 0)
        {
            gameManager.GameFinish();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            gameManager.GameFinish();
        }

        else if(collision.tag == "Coin")
        {
            PlayerData.Coin++;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            if (PlayerData.Rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y+0.5f)
            {
                collision.gameObject.SetActive(false);
            }
            else
            {
                PlayerData.Hp--;
            }
        }
    }

    public void ChangeNormalState(NormalState newState)
    {
        if (currentNormalState != null)
        {
            currentNormalState.Exit();
        }

        currentNormalState = newState;
        currentNormalState.Enter();
    }

    public void ChangeSkillState(SkillState newState)
    {
        if (currentSkillState != null)
        {
            currentSkillState.Exit();
        }

        currentSkillState = newState;
        currentSkillState.Enter();
    }

    public void TakeDamage(int damage)
    {
        PlayerData.Hp -= damage;
    }



}
