using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]float _spawnTime;
    [SerializeField] private int _maxSpawnNum;
    [SerializeField] private Transform[] _spawnPoints;

    private int _spawnNum = 0;
    Coroutine _coroutine;
    WaitForSeconds _coSpawnTime;

    private void Awake()
    {
        _coSpawnTime = new WaitForSeconds(_spawnTime);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnMonster());
    }

    private void Update()
    {
        if(_spawnNum == _maxSpawnNum && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator SpawnMonster()
    {
        while(_spawnNum < _maxSpawnNum)
        {
            GameObject monster = PullManager._pullManager.GetMonster();
            monster.transform.position = _spawnPoints[_spawnNum].position;
            _spawnNum++;

            yield return _coSpawnTime;

        }

        

    }



}
