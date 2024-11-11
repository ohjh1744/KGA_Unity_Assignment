using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// SimulationBehaviour의 경우, 네트워크러너에 붙어서 접속, 해제 ,다른플레이어 참가, 다른 플레이어 나감,등 네트워크 이벤트 관련 일때 상속받음.
public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] GameObject _playerPrefab;
    [Range(0, 10)]
    [SerializeField] float _randomRange;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("새로운 플레이어 참가");

        //LocalPlayer 나 자신
        if (player != Runner.LocalPlayer)
        {
            return;
        }

        Vector3 spawnPos = new Vector3(Random.Range(-_randomRange, _randomRange), 0, Random.Range(-_randomRange, _randomRange));

        //네트워크에서의 Instantiate함수.
        Runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity);
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("플레이어 나감");
    }
}
