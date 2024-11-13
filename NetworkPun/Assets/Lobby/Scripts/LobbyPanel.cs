using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{
    [SerializeField] RectTransform roomContent;
    [SerializeField] RoomEntry roomEntryPrefab;

    private Dictionary<string, RoomEntry> roomDictionary = new Dictionary<string, RoomEntry>();

    //LeaveLobby() 실행 후, LobbySCene에서 OnLeftLobby 호출.
    public void LeaveLobby()
    {
        Debug.Log("로비 퇴장 요청");
        PhotonNetwork.LeaveLobby();
    }

    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // 방이 사라진 경우 + 방이 비공개인 경우 + 입장이 불가능한 방인 경우
            if (info.RemovedFromList == true || info.IsVisible == false || info.IsOpen == false)
            {
                // 예외 상황 : 로비 들어가자마자 사라지는 방인 경우
                if (roomDictionary.ContainsKey(info.Name) == false)
                    continue;

                Destroy(roomDictionary[info.Name].gameObject);
                roomDictionary.Remove(info.Name);
            }

            // 새로운 방이 생성된 경우
            else if (roomDictionary.ContainsKey(info.Name) == false)
            {
                RoomEntry roomEntry = Instantiate(roomEntryPrefab, roomContent);
                roomDictionary.Add(info.Name, roomEntry);
                roomEntry.SetRoomInfo(info);
            }

            // 방의 정보가 변경된 경우
            else if (roomDictionary.ContainsKey(info.Name) == true)
            {
                RoomEntry roomEntry = roomDictionary[info.Name];
                roomEntry.SetRoomInfo(info);
            }
        }
    }

    // LobbyScene에서 OnLeftLobby가 호출 되면 ClearRoomEntries 실행. -> 로비에서 나가면 기존의 모든 방 정보를 없애주기.
    public void ClearRoomEntries()
    {
        foreach (string name in roomDictionary.Keys)
        {
            Destroy(roomDictionary[name].gameObject);
        }
        roomDictionary.Clear();
    }
}
