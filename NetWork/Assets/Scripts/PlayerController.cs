using Fusion;
using UnityEngine;

// 네트워크 오브젝트에 붙어서 게임오브젝트 Update동작관련일때 NetworkBehaviour 상속.
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Vector3 _jumpEndPos;

    [SerializeField] private float _jumpPower;

    private Vector3 _inputDIr;

    private Vector3 _curDir;

    private bool _isJumping;

    private bool _isUp;


    //개인 컴퓨터에서 처리할 작업
    // 본인만 해도 되는 작업, 인벤토리 열기, 메뉴열기 등
    private void Update()
    {
        _inputDIr.x = Input.GetAxisRaw("Horizontal");
        _inputDIr.z = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            _isJumping = true;
            _isUp = true;
        }
    }

    //네트워크에서 처리할 작업
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        Move();
        Jump();
    }

    //네트워크 오브젝트가 생성됐을 때 호출됨, 네트워크상에서의 Awake나 Start대신에 사용.
    public override void Spawned()
    {
        if (HasStateAuthority == true)
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();
            camController._target = transform;
        }
    }

    private void Move()
    {
        _curDir = new Vector3(_inputDIr.x, 0, _inputDIr.z).normalized;
        transform.Translate(_curDir * _moveSpeed * Runner.DeltaTime);
    }


    private void Jump()
    {
        //점프중에만 위 아래 움직임 작동
        if (_isJumping == true)
        {
            // 원래 자리 돌아오면 점핑중 false하고 return
            if (transform.position.y <= 0 + 0.5f && _isUp == false)
            {
                Debug.Log("점프끝!!");
                _isJumping = false;
                return;
            }

            // 점프최대높이지점까지 올라가는중
            if (transform.position.y < _jumpEndPos.y && _isUp == true)
            {
                Debug.Log("올라감!");
                Vector3 newDir = new Vector3(_curDir.x, 1, _curDir.z);
                transform.Translate(newDir * _jumpPower * Runner.DeltaTime);
            }
            // 다올라가면 올라가는중 false
            else
            {
                _isUp = false;
            }

            // 원래자리돌아가기전까지 계속 내려가기
            if (transform.position.y > 0 + 0.5f && _isUp == false)
            {
                Debug.Log("내려감!");
                Vector3 newDir = new Vector3(_curDir.x, -1, _curDir.z);
                transform.Translate(newDir * _jumpPower * Runner.DeltaTime);
            }


        }

    }
}
