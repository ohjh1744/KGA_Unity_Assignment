using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// SimulationBehaviour�� ���, ��Ʈ��ũ���ʿ� �پ ����, ���� ,�ٸ��÷��̾� ����, �ٸ� �÷��̾� ����,�� ��Ʈ��ũ �̺�Ʈ ���� �϶� ��ӹ���.
public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] GameObject _playerPrefab;
    [Range(0, 10)]
    [SerializeField] float _randomRange;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("���ο� �÷��̾� ����");

        //LocalPlayer �� �ڽ�
        if (player != Runner.LocalPlayer)
        {
            return;
        }

        Vector3 spawnPos = new Vector3(Random.Range(-_randomRange, _randomRange), 0, Random.Range(-_randomRange, _randomRange));

        //��Ʈ��ũ������ Instantiate�Լ�.
        Runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity);
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("�÷��̾� ����");
    }
}
