using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviourPunCallbacks
{
    public enum Panel { Login, Menu, Lobby, Room }

    [SerializeField] LoginPanel loginPanel;
    [SerializeField] MainPanel menuPanel;
    [SerializeField] RoomPanel roomPanel;
    [SerializeField] LobbyPanel lobbyPanel;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.InRoom)
        {
            SetActivePanel(Panel.Room);
        }
        else if (PhotonNetwork.InLobby)
        {
            SetActivePanel(Panel.Lobby);
        }
        else if (PhotonNetwork.IsConnected)
        {
            SetActivePanel(Panel.Menu);
        }
        else
        {
            SetActivePanel(Panel.Login);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���ӿ� �����ߴ�!");
        SetActivePanel(Panel.Menu);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"������ �����. cause : {cause}");
        SetActivePanel(Panel.Login);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($"�� ���� ����, ���� : {message}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����");
        SetActivePanel(Panel.Room);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        roomPanel.EnterPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomPanel.ExitPlayer(otherPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        roomPanel.UpdatePlayerProperty(targetPlayer, changedProps);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($"�� ���� ����, ���� : {message}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning($"���� ��Ī ����, ���� : {message}");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("�� ���� ����");
        SetActivePanel(Panel.Menu);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� ����");
        SetActivePanel(Panel.Lobby);
    }

    public override void OnLeftLobby()
    {
        Debug.Log("�κ� ���� ����");
        lobbyPanel.ClearRoomEntries();
        SetActivePanel(Panel.Menu);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ���� ����� ������ �ִ� ��� �������� ������ ������
        // ���ǻ���
        // 1. ó�� �κ� ���� �� : ��� �� ����� ����
        // 2. ���� �� �� ����� ����Ǵ� ��� : ����� �� ��ϸ� ����
        lobbyPanel.UpdateRoomList(roomList);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"{newMasterClient.NickName} �÷��̾ ������ �Ǿ����ϴ�.");
    }

    private void SetActivePanel(Panel panel)
    {
        loginPanel.gameObject.SetActive(panel == Panel.Login);
        menuPanel.gameObject.SetActive(panel == Panel.Menu);
        roomPanel.gameObject.SetActive(panel == Panel.Room);
        lobbyPanel.gameObject.SetActive(panel == Panel.Lobby);
    }
}
