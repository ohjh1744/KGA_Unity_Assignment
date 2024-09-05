using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private MonsterModel _monsterModel;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _remainTime;
    public GameObject Canvas;
    private float _curTime;

    void Start()
    {
        _curTime = _remainTime;
        UpdateHp(_monsterModel.Hp, _monsterModel.MaxHp);

    }

    void Update()
    {
        _curTime -= Time.deltaTime;
        if(_curTime <= 0)
        {
            Canvas.SetActive(false);
        }

    }

    private void OnEnable()
    {
        _monsterModel.OnHpChanged += UpdateHp;

    }

    private void OnDisable()
    {
        _monsterModel.OnHpChanged -= UpdateHp;
        _curTime = _remainTime;

    }


    void UpdateHp(float hp, float maxhp)
    {
        _slider.value = hp / maxhp;
    }


}
