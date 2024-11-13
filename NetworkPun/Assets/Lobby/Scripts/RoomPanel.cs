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

        //Player의 id가 바뀌었을때 호출 되는 이벤트.
        PlayerNumbering.OnPlayerNumberingChanged += UpdatePlayers;

        //localPlayer의 Ready false로 시작하기.
        PhotonNetwork.LocalPlayer.SetReady(false);
    }

    private void OnDisable()
    {
        // 해제
        PlayerNumbering.OnPlayerNumberingChanged -= UpdatePlayers;
    }

    public void UpdatePlayers()
    {
        // 우선 Update하기전에 항상 초기화 부터 진행.
        foreach (PlayerEntry entry in playerEntries)
        {
            entry.SetEmpty();
        }
         
        // 현재 방에 있는 Player들 중에서  
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            // 번호가 없으면 무시.  
            if (player.GetPlayerNumber() == -1)
                continue;

            //번호가 존재한다면
            int number = player.GetPlayerNumber();
            playerEntries[number].SetPlayer(player);
        }

        // 모든 Player가 준비가 다되면, host의 경우  startButton 누를 수 있도록 함.
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startButton.interactable = CheckAllReady();
        }
        else
        {
            startButton.interactable = false;
        }
    }

    // LobbyScene에서 OnPlayerEnteredRoom이 호출 되면, 실행됨. 즉, 다른 Player가 방에 들어온다면.
    // 방에 들어오면서 Player들의 정보 udpate해주기.
    public void EnterPlayer(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} 입장!");
        UpdatePlayers();
    }

    // LobbyScene에서 OnPlayerLeftRoom이 호출 되면, 실행됨. 즉, 다른 Player가 방에서 나간다면.
    // 방에 들어오면서 Player들의 정보 udpate해주기.
    public void ExitPlayer(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} 퇴장!");
        UpdatePlayers();
    }

    //OnPlayerPropertiesUpdate가 호출되면 그에 따라 UpdatePlayerProperty가 실행됨.
    public void UpdatePlayerProperty(Player targetPlayer, Hashtable properties)
    {
        // 레디 커스텀 프로퍼티를 변경한 경우면 READY 키가 있음
        if (properties.ContainsKey(CustomProperty.READY))
        {
            UpdatePlayers();
        }
    }

    // 모든 Player가 Ready 상태인지 확인.
    private bool CheckAllReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetReady() == false)
                return false;
        }

        return true;
    }

    //StartGame 버튼 누를시 게임씬으로 넘어가기.
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
        //게임시작되면 현재 방에 사람들 못들어오도록 닫아두기.
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    // LeaveRoom이 실행되면, LobbyScene의 OnLeftRoom이 호출됨.
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
