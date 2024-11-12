using Photon.Pun;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;

    private void Start()
    {
        idInputField.text = $"Player {Random.Range(1000, 10000)}";
    }

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
