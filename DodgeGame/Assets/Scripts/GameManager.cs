using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Ready, Running, GameOver }
public class GameManager : MonoBehaviour
{

    [SerializeField] GameState _curState;
    [SerializeField] TowerController[] _towers;
    [SerializeField] PlayerController _playerController;
    [SerializeField] Text _text;
    [SerializeField] Text _highRecordText;
    [SerializeField] Text _currentRecordText;
    [SerializeField] float _highRecord;
    [SerializeField] float _currentRecord;
    [SerializeField] float _onCleaerZoneTime;
    [SerializeField] GameObject _button;
    [SerializeField] GameObject _clearZone;

    void Start()
    {
        _curState = GameState.Ready;
        _towers = FindObjectsOfType<TowerController>();
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.OnDied += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        Load();
        ShowText();
        RecordTime();
        OnClearZone();

        if (_curState == GameState.Ready && Input.anyKeyDown)
        {
            GameStart();
        }

    }

    public void ReGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        _curState = GameState.Running;
        foreach (TowerController tower in _towers)
        {
            tower.StartAttack();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _curState = GameState.GameOver;
        _button.SetActive(true);
        foreach (TowerController tower in _towers)
        {
            tower.StopAttack();
        }
    }

    private void ShowText()
    {
        if (_curState == GameState.Ready)
        {
            _text.text = "Ready 상태";
        }
        else if (_curState == GameState.Running)
        {
            _text.text = "Running 상태";
        }
        else if (_curState == GameState.GameOver)
        {
            _text.text = "GameOver 상태";
        }
        _currentRecordText.text = _currentRecord.ToString("F2");
    }


    private void RecordTime()
    {
        if(_curState == GameState.Running)
        {
            _currentRecord += Time.deltaTime;
        }
    }
    public void Save()
    {

        if(_currentRecord > _highRecord)
        {
            PlayerPrefs.SetFloat("HighRecord", _currentRecord);
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("HighRecord") == true)
        {
            _highRecord = PlayerPrefs.GetFloat("HighRecord");
        }
        else
        {
            _highRecord = 0f;
        }
        _highRecordText.text = _highRecord.ToString("F2");
    }

    public void OnClearZone()
    {
        if(_currentRecord > _onCleaerZoneTime)
        {
            _clearZone.SetActive(true);
        }
    }
}
