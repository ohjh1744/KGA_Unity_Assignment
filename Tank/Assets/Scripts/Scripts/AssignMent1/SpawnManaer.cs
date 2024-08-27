using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManaer : MonoBehaviour
{
    private float _spawnTime = 2f;
    private float _spawnLastTime = 0f;
    private int _spawnNum = 0;

    [SerializeField] private Transform[] _spawnPoints;


    private void Update()
    {
        SpawnMonster();
    }


    private void SpawnMonster()
    {
        if (Time.time - _spawnLastTime > _spawnTime && _spawnNum < 10)
        {
            GameObject monster = PullManager._pullManager.GetMonster();
            monster.transform.position = _spawnPoints[_spawnNum].position;
            _spawnLastTime = Time.time;
            _spawnNum++;
        }
    }



}
