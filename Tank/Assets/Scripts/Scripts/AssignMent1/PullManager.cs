using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public static PullManager _pullManager;
    [SerializeField] private Transform _bullets;
    [SerializeField] private Transform _monsters;

    [SerializeField]private GameObject[] bullets;
    private List<GameObject>[] _bulletPools;

    [SerializeField] private GameObject _monster;
    private List<GameObject> _monsterPools;

    void Awake()
    {
        _pullManager = this;
        _bulletPools = new List<GameObject>[bullets.Length];
        _monsterPools = new List<GameObject>();

        for (int i = 0; i < _bulletPools.Length; i++)
        {
            _bulletPools[i] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int index)
    {
        GameObject select = null;

        foreach (GameObject item in _bulletPools[index])
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
            select = Instantiate(bullets[index], _bullets);
            _bulletPools[index].Add(select);
        }

        return select;
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
