using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour 
{
    // 수류탄터지기까지 남은시간
    [SerializeField]private float _remainTime;
    // 폭발하고나서 이펙트가 사라지는 시간
    [SerializeField] private float _disableTime;
    [SerializeField] private GameObject _explosion;
    [SerializeField] AudioClip _audioClip;
    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_disableTime);
    }

    private void Update()
    {
        _remainTime -= Time.deltaTime;
        if(_remainTime < 0)
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(Explode());
            }
        }
    }

    private IEnumerator Explode()
    {
        GameObject explosion = Instantiate(_explosion);
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);
        SoundManager.Instance.PlaySFX(_audioClip);
        yield return _waitForSeconds;

        gameObject.SetActive(false);
        explosion.SetActive(false);
        _coroutine = null;
    }


}
