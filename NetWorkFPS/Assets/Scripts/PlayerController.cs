using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private PlayerModel _playerModel;

    [SerializeField] private PlayerFire _playerFire;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private TMP_Text _hpText; 

    private float _hMove;

    private float _vMove;

    private void Start()
    {
        _hpText.text = _playerModel.Hp.ToString();
    }

    private void OnEnable()
    {
        _playerModel.OnChangedHpEvent += SetHpText;
    }

    private void OnDisable()
    {
        _playerModel.OnChangedHpEvent -= SetHpText;
    }


    public override void Spawned()
    {
        if (HasStateAuthority == true)
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();
            camController.Target = transform;
        }
    }


    private void Update()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");

        //�Ѿ��� 0�� �̻��϶��� �����ϵ��� FIre ����.
        if (Input.GetMouseButtonDown(0) && _playerFire.CurAmmo > 0)
        {
            if(HasStateAuthority == true)
            {
                _playerFire.CurAmmo--;
                _playerFire.Fire();
            }
        }

        //R ������ ����.
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (HasStateAuthority == true)
            {
                _playerFire.ReLoad();
            }
        }
    }


    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }
        Turn();
        Move();
    }

    //ī�޶��� y�� ȸ����ŭ�� Player���� ����.
    private void Turn()
    {
        Quaternion playerRotation = Camera.main.transform.rotation;

        playerRotation.x = 0;

        playerRotation.z = 0;

        transform.rotation = playerRotation;
    }

    //Player�� �ٶ󺸰��ִ� ������ �������� �̵�.
    private void Move()
    {

        Vector3 moveVec = (_vMove * transform.forward ) + (_hMove * transform.right);

        moveVec.y = 0;

        transform.position += moveVec * _moveSpeed * Runner.DeltaTime;

    }

    private void SetHpText(int hp)
    {
        _hpText.text = hp.ToString();
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void TakeHitRpc(int damage)
    {
        Debug.Log("�ǰ�");
        _playerModel.Hp--;
    }
}
