using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] PlayerEntry[] playerEntries;
    [SerializeField] Button startButton;

    private void OnEnable()
    {
        UpdatePlayers();

        //Player�� id�� �ٲ������ ȣ�� �Ǵ� �̺�Ʈ.
        PlayerNumbering.OnPlayerNumberingChanged += UpdatePlayers;

        //localPlayer�� Ready false�� �����ϱ�.
        PhotonNetwork.LocalPlayer.SetReady(false);
    }

    private void OnDisable()
    {
        // ����
        PlayerNumbering.OnPlayerNumberingChanged -= UpdatePlayers;
    }

    public void UpdatePlayers()
    {
        // �켱 Update�ϱ����� �׻� �ʱ�ȭ ���� ����.
        foreach (PlayerEntry entry in playerEntries)
        {
            entry.SetEmpty();
        }
         
        // ���� �濡 �ִ� Player�� �߿���  
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            // ��ȣ�� ������ ����.  
            if (player.GetPlayerNumber() == -1)
                continue;

            //��ȣ�� �����Ѵٸ�
            int number = player.GetPlayerNumber();
            playerEntries[number].SetPlayer(player);
        }

        // ��� Player�� �غ� �ٵǸ�, host�� ���  startButton ���� �� �ֵ��� ��.
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startButton.interactable = CheckAllReady();
        }
        else
        {
            startButton.interactable = false;
        }
    }

    // LobbyScene���� OnPlayerEnteredRoom�� ȣ�� �Ǹ�, �����. ��, �ٸ� Player�� �濡 ���´ٸ�.
    // �濡 �����鼭 Player���� ���� udpate���ֱ�.
    public void EnterPlayer(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} ����!");
        UpdatePlayers();
    }

    // LobbyScene���� OnPlayerLeftRoom�� ȣ�� �Ǹ�, �����. ��, �ٸ� Player�� �濡�� �����ٸ�.
    // �濡 �����鼭 Player���� ���� udpate���ֱ�.
    public void ExitPlayer(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} ����!");
        UpdatePlayers();
    }

    //OnPlayerPropertiesUpdate�� ȣ��Ǹ� �׿� ���� UpdatePlayerProperty�� �����.
    public void UpdatePlayerProperty(Player targetPlayer, Hashtable properties)
    {
        // ���� Ŀ���� ������Ƽ�� ������ ���� READY Ű�� ����
        if (properties.ContainsKey(CustomProperty.READY))
        {
            UpdatePlayers();
        }
    }

    // ��� Player�� Ready �������� Ȯ��.
    private bool CheckAllReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetReady() == false)
                return false;
        }

        return true;
    }

    //StartGame ��ư ������ ���Ӿ����� �Ѿ��.
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
        //���ӽ��۵Ǹ� ���� �濡 ����� ���������� �ݾƵα�.
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    // LeaveRoom�� ����Ǹ�, LobbyScene�� OnLeftRoom�� ȣ���.
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
