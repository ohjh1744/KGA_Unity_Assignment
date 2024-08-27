using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public static PullManager _pullManager;
    [SerializeField] private Transform _monsters;
    [SerializeField] private GameObject _monster;
    private List<GameObject> _monsterPools;

    void Awake()
    {
        _pullManager = this;
        _monsterPools = new List<GameObject>();
    }

    public GameObject GetMonster()
    {
        GameObject select = null;

        foreach (GameObject item in _monsterPools)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(_monster, _monsters);
            _monsterPools.Add(select);
        }

        return select;
    }
}
