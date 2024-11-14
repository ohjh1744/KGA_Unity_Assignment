using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable
{

    [SerializeField] Color[] _colors;

    [SerializeField] Color _color;

    [SerializeField] Renderer _bodyRender;

    [SerializeField] float _speed;

    [SerializeField] Bullet _bulletPrefab;

    [SerializeField] Transform _muzzlePoint;

    private float _vMove;

    private float _hMove;

    private void OnEnable()
    {
        PlayerNumbering.OnPlayerNumberingChanged += SetPlayer;
    }

    private void OnDisable()
    {
        PlayerNumbering.OnPlayerNumberingChanged -= SetPlayer;
    }

    private void Update()
    {
        //�������ڸ� ������ �� �ֵ���
        if (photonView.IsMine == false)
        {
            return;
        }
        Turn();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC(nameof(Fire), RpcTarget.AllViaServer, _muzzlePoint.position, _muzzlePoint.rotation);
        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_bodyRender.material.color.r);
            stream.SendNext(_bodyRender.material.color.g);
            stream.SendNext(_bodyRender.material.color.b);
        }
        else if (stream.IsReading)
        {
            Color color = new Color();
            color.r = (float)stream.ReceiveNext();
            color.g = (float)stream.ReceiveNext();
            color.b = (float)stream.ReceiveNext();

            _bodyRender.material.color = color;
            Debug.Log("���� �ٲ�!");
        }
    }

    private void SetPlayer()
    {
        int number = photonView.Owner.GetPlayerNumber();
        _color = _colors[number];
        _bodyRender.material.color = _color;
    }


    private void Turn()
    {
        Quaternion playerRotation = Camera.main.transform.rotation;

        playerRotation.x = 0;

        playerRotation.z = 0;

        transform.rotation = playerRotation;
    }

    private void Move()
    {

        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (_vMove * transform.forward) + (_hMove * transform.right);

        if (moveDir == Vector3.zero)
        {
            return;
        }

        moveDir.y = 0;

        transform.position += moveDir * _speed * Time.deltaTime;
    }

    [PunRPC]
    private void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        // ����ð� - �����ð� = �����ð�
        float lag = Mathf.Abs((float)PhotonNetwork.Time - (float)info.SentServerTime);

        position += _bulletPrefab.Speed * lag * (rotation * Vector3.forward);
        Bullet bullet = Instantiate(_bulletPrefab, position, rotation);

        //Bullet ��������
        bullet.BulletRenderer.material.color = _bodyRender.material.color;

        //������ �ð���ŭ bullet�� �������� �ð����� ���༭ �������� Ÿ�̹� ����ȭ ���߱�.
        bullet.DesstoryTime -= lag;

    }

}
