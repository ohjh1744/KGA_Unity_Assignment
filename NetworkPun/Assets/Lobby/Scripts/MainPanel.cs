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
        // �⺻������ �׻� CreateRoomPanel�� false ���·� �α�.
        createRoomPanel.SetActive(false);
    }

    //CreateRoomButton ��ư�� OnClick event�� �����.
    public void CreateRoomMenu()
    {
        createRoomPanel.SetActive(true);

        roomNameInputField.text = $"Room {Random.Range(1000, 10000)}";
        maxPlayerInputField.text = "8";
    }

    // RoomOptions�� �̿��Ͽ� ���� Option�� �����ϰ�, PhotonNetwork.CreateRoom ���� -> LobbyScene�� OnCreateRoom �̺�Ʈ�� ȣ���.
    public void CreateRoomConfirm()
    {
        string roomName = roomNameInputField.text;
        if (roomName == "")
        {
            Debug.LogWarning("�� �̸��� �����ؾ� ���� ������ �� �ֽ��ϴ�.");
            return;
        }

        int maxPlayer = int.Parse(maxPlayerInputField.text);
        maxPlayer = Mathf.Clamp(maxPlayer, 1, 8);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayer;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    //CreateRoomPanel�� Cancel ��ư�� Onclick event�� �����.
    public void CreateRoomCancel()
    {
        createRoomPanel.SetActive(false);
    }

    //Randommatching ��ư�� OnClick event�� �����.
    public void RandomMatching()
    {
        Debug.Log("���� ��Ī ��û");

        // ��� �ִ� ���� ������ ���� �ʴ� ���
        // PhotonNetwork.JoinRandomRoom();

        // ��� �ִ� ���� ������ ���� ���� ���� ���� ���
        string name = $"Room {Random.Range(1000, 10000)}";
        RoomOptions options = new RoomOptions() { MaxPlayers = 8 };
        PhotonNetwork.JoinRandomOrCreateRoom(roomName : name, roomOptions : options);
    }

    // PhotonNetwork.JoinLobby()�� ���� �ϸ� LobbyScene��  OnJoinedLobby�̺�Ʈ ȣ��.
    public void JoinLobby()
    {
        Debug.Log("�κ� ���� ��û");
        PhotonNetwork.JoinLobby();
    }

    // PhotonNetwork.DIsconnect()�� ���� �ϸ� LobbyScenedml OnDisconnected�̺�Ʈ ȣ��.
    public void Logout()
    {
        Debug.Log("�α׾ƿ� ��û");
        PhotonNetwork.Disconnect();
    }

    public void CheckForDeleteUser()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;

        if(user.Email != emailInputField.text)
        {
            Debug.LogError("Email�� �߸� �Է��Ͽ����ϴ�.");
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
