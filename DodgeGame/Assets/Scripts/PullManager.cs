using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public static PullManager _pullManager;
    [SerializeField] private GameObject[] _bullets;
    [SerializeField] List<GameObject>[] _bulletPools;

    void Awake()
    {
        _pullManager = this;
        _bulletPools = new List<GameObject>[_bullets.Length];
        for (int i = 0; i < _bullets.Length; i++)
        {
            _bulletPools[i] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int num)
    {
        GameObject select = null;

        foreach (GameObject item in _bulletPools[num])
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
            select = Instantiate(_bullets[num], transform);
            _bulletPools[num].Add(select);
        }

        return select;
    }
}
