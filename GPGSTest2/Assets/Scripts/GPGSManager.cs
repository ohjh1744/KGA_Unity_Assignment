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
        Debug.Log("�ڵ� �α��� �õ�1!");
        int isLogin = PlayerPrefs.GetInt("isLogin");
        
        //������ �α����� �� ���¶��
        if (isLogin == 1)
        {
            Debug.Log("�ڵ��α��� ����!");
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
        //������ �α��� ���� ���� ���¶��, �� �ڵ��α��ν����ϸ� ��ư Ȱ��ȭ
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
        // �α׾ƿ� 
        PlayGamesPlatform.Instance.SignOut();
        //�α��� ���� false�� ����
        PlayerPrefs.SetInt("isLogin", 0);
        //�α��� ��ư �ٽ� ��ȣ�ۿ�On
        _RoginButton.interactable = true;
    }

    public void ShowAllLeaderboard()
    {
        //��� �������� UI ǥ��
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void ShowNumLeaderboard()
    {
        //Num ���� �������� UI ǥ��
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_num_leaderboard);
    }

    public void AddNumLeaderboard()
    {
        _num += 1;
        //Num ���� �������� ������Ʈ
        PlayGamesPlatform.Instance.ReportScore(_num, GPGSIds.leaderboard_num_leaderboard, (bool success) => { Debug.Log("AddNum" + _num); });
    }

    public void ShowTimeLeaderboard()
    {
        //Time ���� �������� UI ǥ��
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_time_leaderboards);
    }
    public void RleaseTimeLeaderboard()
    {
        //ms������ 1000ms�� 1��
        _timne -= 50000;
        Debug.Log("����ð�" + _timne);
        //Num ���� �������� ������Ʈ
        PlayGamesPlatform.Instance.ReportScore(_timne, GPGSIds.leaderboard_time_leaderboards, (bool success) => { Debug.Log("AddTime" + _timne);  });
    }

    public void ShowAllAchievementUI()
    {
        //��ü ���� UIǥ��
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void AddLevelAchievement()
    {
        //�ܰ躰 ���� ����, ����°���ڰ��� ���� �� ��ȯ�� �Լ� �ۼ�.
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_levelach, 1, (bool success) => { });
    }

    public void UnlockNormalAchievement()
    {
        //�Ϲ� ���� Ŭ����, 
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_normalach,  (bool success) => { });
    }


}
