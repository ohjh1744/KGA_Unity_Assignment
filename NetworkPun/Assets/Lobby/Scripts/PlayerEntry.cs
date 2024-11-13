using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] TMP_Text readyText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Button readyButton;


    // Player의 이름 및 Ready 상태 show.
    public void SetPlayer(Player player)
    {
        //host면 이름 앞에 Master 붙여주고, 아니면 말기
        if (player.IsMasterClient)
        {
            nameText.text = $"Master\n{player.NickName}";
        }
        else
        {
            nameText.text = player.NickName;
        }
        readyButton.gameObject.SetActive(true);
        // 본인 버튼만 누를수 있도록
        readyButton.interactable = player == PhotonNetwork.LocalPlayer;
        // REady상태에 따라 text 변환해주기.
        if (player.GetReady())
        {
            readyText.text = "Ready";
        }
        else
        {
            readyText.text = "";
        }
    }

    // 초기화
    public void SetEmpty()
    {
        readyText.text = "";
        nameText.text = "None";
        readyButton.gameObject.SetActive(false);
    }


    // Ready button과 연결되어있으며, Ready상태에서 누르면 false, 아닌상태에서 누르면 true.
    //SetReady를 통해 LobbyScene의 OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) 이벤트가 발동.
    public void Ready()
    {
        bool ready = PhotonNetwork.LocalPlayer.GetReady();
        if (ready)
        {
            PhotonNetwork.LocalPlayer.SetReady(false);
        }
        else
        {
            PhotonNetwork.LocalPlayer.SetReady(true);
        }
    }
}
