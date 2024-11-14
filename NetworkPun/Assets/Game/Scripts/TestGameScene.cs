using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestGameScene : MonoBehaviourPunCallbacks
{

    public const string RoomName = "TestRoom";

    private Coroutine spawnRoutine;
    void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = $"Player {Random.Range(1000, 10000)}";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;
        options.IsVisible = false;

        PhotonNetwork.JoinOrCreateRoom(RoomName, options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(StartDelayRoutine());
    }

    IEnumerator StartDelayRoutine()
    {
        yield return new WaitForSeconds(1f);
        TestGameStart();
    }
    public void TestGameStart()
    {
        Debug.Log("���� ����");
        
        //�׽�Ʈ�� ���� ���� �κ�
        PlayerSpawn();

    
        if(PhotonNetwork.IsMasterClient == false)
        {
            return;
        }

        //���常 ������ �� �ִ� �ڵ�
        spawnRoutine = StartCoroutine(MonsterSpawnRoutine());
    }

    //������ �ٲ�� �Ǹ� ���ο� ������ ������ 
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (newMasterClient.IsLocal)
        {
            spawnRoutine = StartCoroutine(MonsterSpawnRoutine());
        }
    }

    private void PlayerSpawn()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

        Color color = Random.ColorHSV();
        object[] data = { color.r, color.g, color.b };

        PhotonNetwork.Instantiate("Player", randomPos, Quaternion.identity, data: data);
    }

    IEnumerator MonsterSpawnRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);

        while (true)
        {
            yield return delay;
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            PhotonNetwork.InstantiateRoomObject("Monster", randomPos, Quaternion.identity);
        }
        //for(int i = 0; i < 3; i++)
        //{
        //    Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        //    PhotonNetwork.InstantiateRoomObject("Monster", randomPos, Quaternion.identity);
        //}
    }


}
