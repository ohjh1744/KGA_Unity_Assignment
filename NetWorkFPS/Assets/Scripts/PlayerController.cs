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

        //총알이 0개 이상일때만 가능하도록 FIre 가능.
        if (Input.GetMouseButtonDown(0) && _playerFire.CurAmmo > 0)
        {
            if(HasStateAuthority == true)
            {
                _playerFire.CurAmmo--;
                _playerFire.Fire();
            }
        }

        //R 누르면 장전.
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

    //카메라의 y축 회전만큼만 Player에게 적용.
    private void Turn()
    {
        Quaternion playerRotation = Camera.main.transform.rotation;

        playerRotation.x = 0;

        playerRotation.z = 0;

        transform.rotation = playerRotation;
    }

    //Player가 바라보고있는 방향을 기준으로 이동.
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
        Debug.Log("피격");
        _playerModel.Hp--;
    }
}
