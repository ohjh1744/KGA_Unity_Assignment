using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float _startTime;
    private float _lastTime = 0;
    [SerializeField]private float _missileSpeed;
    public static int NumMissile = 0;

    void Awake()
    {
        NumMissile++;
        _startTime = Time.time;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * _missileSpeed * Time.deltaTime, Space.Self);
        Disappear();
    }

    private void Disappear()
    {
        _lastTime = Time.time - _startTime;
        if (_lastTime > 4f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        NumMissile--;
    }


}
