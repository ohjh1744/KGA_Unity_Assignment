using Photon.Pun;
using TMPro;
using UnityEngine;

// ���� �α��� ȭ�� ���� ����
public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;

    private void Start()
    {
        //�⺻������ Player���� �������� Id�� ����������.
        idInputField.text = $"Player {Random.Range(1000, 10000)}";
    }

    //�г����� �����ϰ�, PhotonNetwork.ConnectUsingSettings()�� ���� ���� �õ��� ��û�ϸ�, LobbyScene�� OnConnectedToMaster���� �̺�Ʈ�� ���� ȣ������ �Ϸ�.
    public void Login()
    {
        if (idInputField.text == "")
        {
            Debug.LogWarning("���̵� �Է��ؾ� ������ �����մϴ�.");
            return;
        }

        // ������ ��û
        // PhotonNetwork.~~~ �� ������ ��û�� ������ �� ����
        PhotonNetwork.LocalPlayer.NickName = idInputField.text; // �÷��̾� �г��� ����
        Debug.Log("�α��� ��û");
        PhotonNetwork.ConnectUsingSettings();                   // ���� ���������� �������� ���� ��û
    }
}
