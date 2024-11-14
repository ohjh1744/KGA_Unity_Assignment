using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

//Ȯ�� �޼ҵ� Ȱ��.
public static class CustomProperty
{
    public const string READY = "Ready";

    private static PhotonHashtable customProperty = new PhotonHashtable();

    // Ư�� Player�� Ready ���¸� ��ȯ.
    // Player�� property�Ӽ��� �ٲ�� LobbyScene�� OnPlayerPropertiesUpdate�� ȣ���.
    public static void SetReady(this Player player, bool ready)
    {
        customProperty.Clear();
        customProperty[READY] = ready;
        
        // PhotonHashtable�� �̿��� Ư�� Player�� Ready�� �߰�.
        player.SetCustomProperties(customProperty);
    }

    // Ư�� Player�� Ready ���¸� ������.
    public static bool GetReady(this Player player)
    {
        PhotonHashtable customProperty = player.CustomProperties;
        // Ư�� Player�� Ready�Ӽ��� �����ϴ� ���(��, REady���� ���� ������ �Ϸ�� ����)
        if (customProperty.ContainsKey(READY))
        {
            return (bool)customProperty[READY];
        }
        else
        {
            return false;
        }
    }

    public const string LOAD = "Load";

    public static void SetLoad(this Player player, bool load)
    {
        PhotonHashtable customProperty = new PhotonHashtable();
        customProperty[LOAD] = load;
        player.SetCustomProperties(customProperty);
    }

    public static bool GetLoad(this Player player)
    {
        PhotonHashtable customProperty = player.CustomProperties;
        if (customProperty.ContainsKey(LOAD))
        {
            return (bool)customProperty[LOAD];
        }
        else
        {
            return false;
        }
    }
}
