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

    [SerializeField] private List<UnitData> _unitData;
    public List<UnitData> UnitData{  get { return _unitData; } set { _unitData = value; } }

    public void SetClear()
    {
        _gold = 1;
        _time = 3;
        _isClear = false;
    }
}

[System.Serializable]
public class UnitData
{
    public string Name;

    public int Level;
}
