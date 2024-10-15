using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private float _currentTIme;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTIme += Time.deltaTime;
        if(_currentTIme > _destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
