using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Auth;

public class MainPanel : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField maxPlayerInputField;
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] GameObject checkForDeletePanel;
    [SerializeField] GameObject realDeletePanel;

    private void OnEnable()
    {
        // 기본적으로 항상 CreateRoomPanel을 false 상태로 두기.
        createRoomPanel.SetActive(false);
    }

    //CreateRoomButton 버튼의 OnClick event와 연결됨.
    public void CreateRoomMenu()
    {
        createRoomPanel.SetActive(true);

        roomNameInputField.text = $"Room {Random.Range(1000, 10000)}";
        maxPlayerInputField.text = "8";
    }

    // RoomOptions를 이용하여 방의 Option을 설정하고, PhotonNetwork.CreateRoom 실행 -> LobbyScene의 OnCreateRoom 이벤트가 호출됨.
    public void CreateRoomConfirm()
    {
        string roomName = roomNameInputField.text;
        if (roomName == "")
        {
            Debug.LogWarning("방 이름을 지정해야 방을 생성할 수 있습니다.");
            return;
        }

        int maxPlayer = int.Parse(maxPlayerInputField.text);
        maxPlayer = Mathf.Clamp(maxPlayer, 1, 8);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayer;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    //CreateRoomPanel의 Cancel 버튼의 Onclick event와 연결됨.
    public void CreateRoomCancel()
    {
        createRoomPanel.SetActive(false);
    }

    //Randommatching 버튼의 OnClick event와 연결됨.
    public void RandomMatching()
    {
        Debug.Log("랜덤 매칭 요청");

        // 비어 있는 방이 없으면 들어가지 않는 방식
        // PhotonNetwork.JoinRandomRoom();

        // 비어 있는 방이 없으면 새로 방을 만들어서 들어가는 방식
        string name = $"Room {Random.Range(1000, 10000)}";
        RoomOptions options = new RoomOptions() { MaxPlayers = 8 };
        PhotonNetwork.JoinRandomOrCreateRoom(roomName : name, roomOptions : options);
    }

    // PhotonNetwork.JoinLobby()를 실행 하면 LobbyScene의  OnJoinedLobby이벤트 호출.
    public void JoinLobby()
    {
        Debug.Log("로비 입장 요청");
        PhotonNetwork.JoinLobby();
    }

    // PhotonNetwork.DIsconnect()를 실행 하면 LobbyScenedml OnDisconnected이벤트 호출.
    public void Logout()
    {
        Debug.Log("로그아웃 요청");
        PhotonNetwork.Disconnect();
    }

    public void CheckForDeleteUser()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;

        if(user.Email != emailInputField.text)
        {
            Debug.LogError("Email을 잘못 입력하였습니다.");
            return;
        }

        realDeletePanel.SetActive(true);

    }

    public void DeleteUser()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;

        user.DeleteAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("DeleteAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("User deleted successfully.");
            PhotonNetwork.Disconnect();
        });

        checkForDeletePanel.SetActive(false);
        realDeletePanel.SetActive(false);
        gameObject.SetActive(false);


    }
}
