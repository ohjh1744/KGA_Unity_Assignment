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

            _userInfoText.text = $"�ڵ� �α��� ����{displayName}, {userID}";

        }
        else
        {
            _userInfoText.text = $"�α��� ����";
        }
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

    public void SaveData()
    {
        //���Ƿ� ���̺��� Data Update
        _gameData.Gold += 1;

        //Ŭ���� ����ҿ� ��ȣ�ۿ� ������ �������̽�
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        // 1��° ���� �����̸�, 2��° ���� ĳ�ÿ� �����Ͱ� ���ų� �ֽ� �����Ͱ� �ƴ϶�� ��Ʈ��ũ�� ���� �ҷ���,
        // 3��° ���� �������� ���������� ����� ������ ������, 4��° ���� �ݹ� �Լ�
        savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, OnSaveDataOpend);
    }

    //����� ���� �����Ϳ� ���� ��û ��� ���¸� �ٷ� �������̽�, ����� ������ ��Ÿ �����͸� �ٷ� �������̽�
    private void OnSaveDataOpend(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save���� ����");

            var update = new SavedGameMetadataUpdate.Builder().Build();

            //json
            var json = JsonUtility.ToJson(_gameData);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            Debug.Log($"���� ������: {bytes}");

            savedGameClient.CommitUpdate(game, update, bytes, OnSaveDataWritten);


        }
        else
        {
            Debug.Log("Save���� ����");
            Debug.Log($"{status}");
        }

    }

    private void OnSaveDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("���� ����");
            _userInfoText.text = "Save Clear";
        }
        else
        {
            Debug.Log("���� ����");
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
            Debug.Log("Load ���� ����");

            savedGameClient.ReadBinaryData(data, OnLoadDataRead);
        }
        else
        {
            Debug.Log("Load ���� ����");
        }
    }

    private void OnLoadDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        string data = Encoding.UTF8.GetString(loadedData);

        if(data == "")
        {
            Debug.Log("����� �����Ͱ� ����");
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
            Debug.Log("������ ���� ����");
            _userInfoText.text = "Delete Clear";
            _gameData.SetClear();
        }
        else
        {
            Debug.Log("������ ���� ����");
            _userInfoText.text = "Delete Fail";
        }
    }

    IEnumerator CheckForUpdate()
    {
        Debug.Log("Check!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //�ξ� ������Ʈ ������ ���� Ŭ���� �ν��Ͻ�ȭ
        _appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
          _appUpdateManager.GetAppUpdateInfo();

        // Wait until the asynchronous operation completes.
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            //������Ʈ ���� ���¶�� 
            if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

                var startUpdateRequest = _appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                //�ٿ�ޱ�
                while (!startUpdateRequest.IsDone)
                {
                    if(startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        Debug.Log("������Ʈ �ٿ�ε� ������");
                    }
                    else if(startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.Log("�ٿ�� �Ϸ�");
                    }
                    yield return null;
                }

                //���� ��ġ
                var result = _appUpdateManager.CompleteUpdate();

                //�Ϸ�Ǿ����� ������ Ȯ��
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return (int)startUpdateRequest.Status;
            }
            //������Ʈ�� ���� ���¶��
            else if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.Log("������Ʈ ����!");
                //�����г� ���ֱ�
                _mainPanel.SetActive(true);
                //�α��� �õ�
                Login();
            }
        }
        else
        {
            Debug.Log("������Ʈ ����" + appUpdateInfoOperation.Error);
        }
    }


}
