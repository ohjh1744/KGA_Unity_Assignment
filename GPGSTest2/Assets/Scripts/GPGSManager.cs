using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userInfoText;
    [SerializeField] private Button _RoginButton;

    [SerializeField] private int _num;
    [SerializeField] private long _timne;

    private static string _fileName = "file.dat";
    [SerializeField] private  GameData _gameData;

    private void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        // 설정 gpgs에 반영 및 초기화
        PlayGamesPlatform.InitializeInstance(config);
        // gpgs 활성화
        PlayGamesPlatform.Activate();
    }

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

    public void SaveData()
    {
        //임의로 세이브할 Data Update
        _gameData.Gold += 1;

        //클라우드 저장소와 상호작용 가능한 인터페이스
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        // 1번째 인자 파일이름, 2번째 인자 캐시에 데이터가 없거나 최신 데이터가 아니라면 네트워크를 통해 불러옴,
        // 3번째 인자 마지막에 정상적으로 저장된 정보를 가져옴, 4번째 인자 콜백 함수
        savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, OnSaveDataOpend);
    }

    //저장된 게임 데이터에 대한 요청 결과 상태를 다룬 인터페이스, 저장된 게임의 메타 데이터를 다룬 인터페이스
    private void OnSaveDataOpend(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save열기 성공");

            var update = new SavedGameMetadataUpdate.Builder().Build();

            //json
            var json = JsonUtility.ToJson(_gameData);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            Debug.Log($"저장 데이터: {bytes}");

            savedGameClient.CommitUpdate(game, update, bytes, OnSaveDataWritten);


        }
        else
        {
            Debug.Log("Save열기 실패");
            Debug.Log($"{status}");
        }

    }

    private void OnSaveDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("저장 성공");
            _userInfoText.text = "Save Clear";
        }
        else
        {
            Debug.Log("저장 실패");
            _userInfoText.text = "Save Fail";
        }
    }

    public void LoadData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, OnLoadDataOpend);
    }

    private void OnLoadDataOpend(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Load 열기 성공");

            savedGameClient.ReadBinaryData(data, OnLoadDataRead);
        }
        else
        {
            Debug.Log("Load 열기 실패");
        }
    }

    private void OnLoadDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        string data = Encoding.UTF8.GetString(loadedData);

        if(data == "")
        {
            Debug.Log("저장된 데이터가 없음");
            _userInfoText.text = "NoSaveData";
        }
        else
        {
            Debug.Log($"Load Read Data: {data}");
            _userInfoText.text = "YesSaveData";

            //json
            _gameData = JsonUtility.FromJson<GameData>(data);
        }
    }


    public void DeleteData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, OnDeleteSaveData);
    }

    private void OnDeleteSaveData(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            savedGameClient.Delete(data);
            Debug.Log("데이터 삭제 성공");
            _userInfoText.text = "Delete Clear";
        }
        else
        {
            Debug.Log("데이터 삭제 실패");
            _userInfoText.text = "Delete Fail";
        }
    }


}
