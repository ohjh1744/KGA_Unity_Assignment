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
    [SerializeField] private Button _RoginButton;

    [SerializeField] private int _num;
    [SerializeField] private long _timne;

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
            _RoginButton.interactable = true;
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

    public void GPGS_Logout()
    {
        _userInfoText.text = "LogOut";
        // 로그아웃 
        PlayGamesPlatform.Instance.SignOut();
        //로그인 상태 false로 변경
        PlayerPrefs.SetInt("isLogin", 0);
        //로그인 버튼 다시 상호작용On
        _RoginButton.interactable = true;
    }

    public void ShowAllLeaderboard()
    {
        //모든 리더보드 UI 표시
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void ShowNumLeaderboard()
    {
        //Num 형식 리더보드 UI 표시
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_num_leaderboard);
    }

    public void AddNumLeaderboard()
    {
        _num += 1;
        //Num 형식 리더보드 업데이트
        PlayGamesPlatform.Instance.ReportScore(_num, GPGSIds.leaderboard_num_leaderboard, (bool success) => { Debug.Log("AddNum" + _num); });
    }

    public void ShowTimeLeaderboard()
    {
        //Time 형식 리더보드 UI 표시
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_time_leaderboards);
    }
    public void RleaseTimeLeaderboard()
    {
        //ms단위로 1000ms면 1초
        _timne -= 50000;
        Debug.Log("현재시간" + _timne);
        //Num 형식 리더보드 업데이트
        PlayGamesPlatform.Instance.ReportScore(_timne, GPGSIds.leaderboard_time_leaderboards, (bool success) => { Debug.Log("AddTime" + _timne);  });
    }

    public void ShowAllAchievementUI()
    {
        //전체 업적 UI표시
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void AddLevelAchievement()
    {
        //단계별 업적 증가, 세번째인자값은 성공 시 반환할 함수 작성.
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_levelach, 1, (bool success) => { });
    }

    public void UnlockNormalAchievement()
    {
        //일반 업적 클리어, 
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_normalach,  (bool success) => { });
    }


}
