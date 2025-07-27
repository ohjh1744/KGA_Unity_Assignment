using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    [SerializeField] private int _gold;
    public int Gold { get { return _gold; } set { _gold = value; } }

    [SerializeField]  private float _time;
    public float Time { get { return _time; } set { _time = value; } }

    [SerializeField] private  bool _isClear = false;
    public bool IsClear { get { return _isClear; } set { _isClear = value; } }

    [SerializeField] private float[] _skillTime;
    public float[] SkillTime {  get { return _skillTime; } set { _skillTime = value; } }
}
