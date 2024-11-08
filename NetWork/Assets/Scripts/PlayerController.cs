using Fusion;
using UnityEngine;

// ��Ʈ��ũ ������Ʈ�� �پ ���ӿ�����Ʈ Update���۰����϶� NetworkBehaviour ���.
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Vector3 _jumpEndPos;

    [SerializeField] private float _jumpPower;

    private Vector3 _inputDIr;

    private Vector3 _curDir;

    private bool _isJumping;

    private bool _isUp;


    //���� ��ǻ�Ϳ��� ó���� �۾�
    // ���θ� �ص� �Ǵ� �۾�, �κ��丮 ����, �޴����� ��
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

    //��Ʈ��ũ���� ó���� �۾�
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        Move();
        Jump();
    }

    //��Ʈ��ũ ������Ʈ�� �������� �� ȣ���, ��Ʈ��ũ�󿡼��� Awake�� Start��ſ� ���.
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
        //�����߿��� �� �Ʒ� ������ �۵�
        if (_isJumping == true)
        {
            // ���� �ڸ� ���ƿ��� ������ false�ϰ� return
            if (transform.position.y <= 0 + 0.5f && _isUp == false)
            {
                Debug.Log("������!!");
                _isJumping = false;
                return;
            }

            // �����ִ������������ �ö󰡴���
            if (transform.position.y < _jumpEndPos.y && _isUp == true)
            {
                Debug.Log("�ö�!");
                Vector3 newDir = new Vector3(_curDir.x, 1, _curDir.z);
                transform.Translate(newDir * _jumpPower * Runner.DeltaTime);
            }
            // �ٿö󰡸� �ö󰡴��� false
            else
            {
                _isUp = false;
            }

            // �����ڸ����ư��������� ��� ��������
            if (transform.position.y > 0 + 0.5f && _isUp == false)
            {
                Debug.Log("������!");
                Vector3 newDir = new Vector3(_curDir.x, -1, _curDir.z);
                transform.Translate(newDir * _jumpPower * Runner.DeltaTime);
            }


        }

    }
}
