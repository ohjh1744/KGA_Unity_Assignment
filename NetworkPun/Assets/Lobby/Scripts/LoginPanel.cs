using Photon.Pun;
using TMPro;
using UnityEngine;

// 먼저 로그인 화면 부터 시작
public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;

    private void Start()
    {
        //기본적으로 Player숫자 형식으로 Id가 정해져있음.
        idInputField.text = $"Player {Random.Range(1000, 10000)}";
    }

    //닉네임을 설정하고, PhotonNetwork.ConnectUsingSettings()을 통해 접속 시도를 요청하면, LobbyScene의 OnConnectedToMaster에서 이벤트를 통해 호출접속 완료.
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
