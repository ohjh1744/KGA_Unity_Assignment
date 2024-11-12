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
            Debug.LogWarning("아이디를 입력해야 접속이 가능합니다.");
            return;
        }

        // 서버에 요청
        // PhotonNetwork.~~~ 로 서버에 요청을 진행할 수 있음
        PhotonNetwork.LocalPlayer.NickName = idInputField.text; // 플레이어 닉네임 설정
        Debug.Log("로그인 요청");
        PhotonNetwork.ConnectUsingSettings();                   // 포톤 설정파일을 내용으로 접속 신청
    }
}
