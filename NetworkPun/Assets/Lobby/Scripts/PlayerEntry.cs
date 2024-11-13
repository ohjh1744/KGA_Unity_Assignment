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


    // Player�� �̸� �� Ready ���� show.
    public void SetPlayer(Player player)
    {
        //host�� �̸� �տ� Master �ٿ��ְ�, �ƴϸ� ����
        if (player.IsMasterClient)
        {
            nameText.text = $"Master\n{player.NickName}";
        }
        else
        {
            nameText.text = player.NickName;
        }
        readyButton.gameObject.SetActive(true);
        // ���� ��ư�� ������ �ֵ���
        readyButton.interactable = player == PhotonNetwork.LocalPlayer;
        // REady���¿� ���� text ��ȯ���ֱ�.
        if (player.GetReady())
        {
            readyText.text = "Ready";
        }
        else
        {
            readyText.text = "";
        }
    }

    // �ʱ�ȭ
    public void SetEmpty()
    {
        readyText.text = "";
        nameText.text = "None";
        readyButton.gameObject.SetActive(false);
    }


    // Ready button�� ����Ǿ�������, Ready���¿��� ������ false, �ƴѻ��¿��� ������ true.
    //SetReady�� ���� LobbyScene�� OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) �̺�Ʈ�� �ߵ�.
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
