using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _roomName = "TestRoom";
    [SerializeField] private int _maxPlayerNum;
    [SerializeField] private CameraController _camera;
   

    void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = $"Player{Random.Range(100, 10000)}";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Matser 辑滚 立加");
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = _maxPlayerNum;
        options.IsVisible = false;

        PhotonNetwork.JoinOrCreateRoom(_roomName, options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Matser 辑滚 立加");
        StartGame();
    }

    private void StartGame()
    {
        PlayerSpawn();
    }

    private void PlayerSpawn()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        GameObject player = PhotonNetwork.Instantiate("Player", randomPos, Quaternion.identity);
        _camera.Target = player.transform;
    }
}
