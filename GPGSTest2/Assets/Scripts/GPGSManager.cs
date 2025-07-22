using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userInfoText;
    [SerializeField] private Button _button;

    private void Start()
    {
        GPGS_AutoLogin();
    }

    private void GPGS_AutoLogin()
    {
        Debug.Log("자동 로그인 시도1!");
        int isLogin = PlayerPrefs.GetInt("isLogin");
        
        //이전에 로그인을 한 상태라면
        if (isLogin == 1)
        {
            Debug.Log("자동로그인 성공!");
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {

                if (result == SignInStatus.Success)
                {
                 
                    string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                    string id = PlayGamesPlatform.Instance.GetUserId();
                    string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

                    _userInfoText.text = $"Auto Sucess {name}";
                }
                else
                {
                    _userInfoText.text = "Auto Failed ";
                }
            });
        }
        //이전에 로그인 하지 않은 상태라면, 즉 자동로그인실패하면 버튼 활성화
        else
        {
            _button.interactable = true;
        }
    }

    public void GPGS_ManualLogin()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {

            if (result == SignInStatus.Success)
            {
                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                string id = PlayGamesPlatform.Instance.GetUserId();
                string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

                PlayerPrefs.SetInt("isLogin", 1);
                _userInfoText.text = "Manual Sucess \n" + name;
            }
            else
            {
                _userInfoText.text = "Manaul Failed ";
            }
        });
    }


}
