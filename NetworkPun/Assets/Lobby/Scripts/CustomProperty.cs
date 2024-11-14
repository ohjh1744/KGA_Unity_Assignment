using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

//확장 메소드 활용.
public static class CustomProperty
{
    public const string READY = "Ready";

    private static PhotonHashtable customProperty = new PhotonHashtable();

    // 특정 Player의 Ready 상태를 변환.
    // Player의 property속성이 바뀌며 LobbyScene의 OnPlayerPropertiesUpdate이 호출됨.
    public static void SetReady(this Player player, bool ready)
    {
        customProperty.Clear();
        customProperty[READY] = ready;
        
        // PhotonHashtable을 이용해 특정 Player의 Ready값 추가.
        player.SetCustomProperties(customProperty);
    }

    // 특정 Player의 Ready 상태를 가져옴.
    public static bool GetReady(this Player player)
    {
        PhotonHashtable customProperty = player.CustomProperties;
        // 특정 Player의 Ready속성이 존재하는 경우(즉, REady상태 관련 세팅이 완료된 상태)
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
