using Google.Play.AppUpdate;
using Google.Play.Common;
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
    [SerializeField] private TextMeshProUGUI _dataDetailText;

    [SerializeField] private int _num;
    [SerializeField] private long _timne;

    private static string _fileName = "file.dat";
    [SerializeField] private  GameData _gameData;


    [SerializeField] private GameObject _mainPanel;
    private AppUpdateManager _appUpdateManager;


    private void Start()
    {
        StartCoroutine(CheckForUpdate());
    }

    private void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string userID = PlayGamesPlatform.Instance.GetUserId();

            _userInfoText.text = $"자동 로그인 성공{displayName}, {userID}";

        }
        else
        {
            _userInfoText.text = $"로그인 실패";
        }
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
            _dataDetailText.text = "X";
        }
        else
        {
            Debug.Log($"Load Read Data: {data}");
            _userInfoText.text = "YesSaveData";

            //json
            _gameData = JsonUtility.FromJson<GameData>(data);
            _dataDetailText.text = $"{data}";
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
            _gameData.SetClear();
        }
        else
        {
            Debug.Log("데이터 삭제 실패");
            _userInfoText.text = "Delete Fail";
        }
    }

    IEnumerator CheckForUpdate()
    {
        Debug.Log("Check!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //인앱 업데이트 관리를 위한 클래스 인스턴스화
        _appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
          _appUpdateManager.GetAppUpdateInfo();

        // Wait until the asynchronous operation completes.
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            //업데이트 가능 상태라면 
            if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

                var startUpdateRequest = _appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                //다운받기
                while (!startUpdateRequest.IsDone)
                {
                    if(startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        Debug.Log("업데이트 다운로드 진행중");
                    }
                    else if(startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.Log("다운르도 완료");
                    }
                    yield return null;
                }

                //실제 설치
                var result = _appUpdateManager.CompleteUpdate();

                //완료되었는지 마지막 확인
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return (int)startUpdateRequest.Status;
            }
            //업데이트가 없는 상태라면
            else if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.Log("업데이트 없음!");
                //메인패널 켜주기
                _mainPanel.SetActive(true);
                //로그인 시도
                Login();
            }
        }
        else
        {
            Debug.Log("업데이트 오류" + appUpdateInfoOperation.Error);
        }
    }


}
