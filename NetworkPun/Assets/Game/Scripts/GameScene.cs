using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviourPunCallbacks
{
    [SerializeField] Button gameOverButton;
    [SerializeField] TMP_Text countDwonText;

    private void Start()
    {
        PhotonNetwork.LocalPlayer.SetLoad(true);

        if(PhotonNetwork.LocalPlayer.GetPlayerNumber() == 1)
        {
            //����Ƽ���� ������ sleep�� ���� ����ϸ� �ȵ�.
            Thread.Sleep(3000);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            gameOverButton.interactable = true;
        }
        else
        {
            gameOverButton.interactable = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gameOverButton.interactable = true;
        }
        else
        {
            gameOverButton.interactable = false;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey(CustomProperty.LOAD))
        {
            Debug.Log($"{targetPlayer.NickName}�� �ε��� �Ϸ�Ǿ���.");
            bool allLoaded = CheckAllLoad();
            Debug.Log($"��� �÷��̾ �ε� �Ϸ�Ǿ��°�: {allLoaded}");
            if (allLoaded)
            {
                GameStart();
            }
        }
    }

    public void GameOver()
    {
        if (PhotonNetwork.IsMasterClient == false)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void GameStart()
    {
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        for(int i = 3; i > 0; i--)
        {
            countDwonText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDwonText.text = "Game Start!";
        Debug.Log("���� ����!");
    }

    private bool CheckAllLoad()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player.GetLoad() == false)
            {
                return false;
            }
        }

        return true;
    }
}
