using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float speed;

    [SerializeField] Color[] colors;
    [SerializeField] Color color;
    [SerializeField] Renderer bodyRender;

    private void Start()
    {
        int number = photonView.Owner.GetPlayerNumber();
        color = colors[number];

        //object[] data = photonView.InstantiationData;
        //color.r = (float)data[0];
        //color.g = (float)data[1];
        //color.b = (float)data[2];

        bodyRender.material.color = color;

    }
    private void Update()
    {
        //소유권자만 실행할 수 있도록
        if (photonView.IsMine == false)
        {
            return;
        }
       
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC(nameof(Fire), RpcTarget.AllViaServer, muzzlePoint.position, muzzlePoint.rotation);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(speed);
        }
        else if (stream.IsReading)
        {
            speed = (float)stream.ReceiveNext();
        }
    }


    private void Move()
    {
        Vector3 moveDir = new Vector3();
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.z = Input.GetAxisRaw("Vertical");

        if (moveDir == Vector3.zero)
        {
            return;
        }

        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = moveDir.normalized;
    }


    [PunRPC]
    private void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        // 현재시간 - 보낸시간 = 지연시간
        float lag = Mathf.Abs((float)PhotonNetwork.Time - (float)info.SentServerTime);

        position += bulletPrefab.Speed * lag * (rotation * Vector3.forward);
        Instantiate(bulletPrefab, position, rotation);
    }


}
