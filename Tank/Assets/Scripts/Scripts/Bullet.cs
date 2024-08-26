using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private BulletNum _bulletNum;
    private float remainTime = 10f;
    public static int[] MaxBullets = {0, 0, 0 };

    private void OnEnable()
    {
        MaxBullets[(int)_bulletNum]++;
    }
   
    void Update()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime, Space.Self);
        remainTime -= Time.deltaTime;
        if(remainTime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        remainTime = 10f;
        MaxBullets[(int)_bulletNum]--;
    }
}
