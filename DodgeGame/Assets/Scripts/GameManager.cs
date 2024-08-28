using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameState { Ready, Running, GameOver}
public class GameManager : MonoBehaviour
{
    [SerializeField] GameState _curState;
    [SerializeField] TowerController[] _towers;
    [SerializeField] PlayerController _playerController;
    [SerializeField] Text _text;
    [SerializeField] GameObject _button;

    [SerializeField]private float _timer;
    [SerializeField] private float _maxTimer;
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
        ShowText();
        _timer += Time.deltaTime;
        if(_timer > _maxTimer)
        {
            GameOver();
        }

        if(_curState == GameState.Ready && Input.anyKeyDown)
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
        foreach(TowerController tower in _towers)
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

    public void ShowText()
    {
        if(_curState == GameState.Ready)
        {
            _text.text = "Ready 상태";
        }
        else if(_curState == GameState.Running)
        {
            _text.text = "Running 상태";
        }
        else if(_curState == GameState.GameOver)
        {
            _text.text = "GameOver 상태";
        }
    }
}
