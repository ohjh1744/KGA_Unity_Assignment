using System;
using System.Collections;
using UnityEngine;

public class FireState : MonoBehaviour
{
    [SerializeField] private GameObject _fireFlash;
    [SerializeField] private GameObject _hitImpact;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _attackTime;

    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds;
    private float _lastAttackTime;
    [SerializeField] AudioClip _audioClip;
    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_attackTime * 0.5f);
    }
    private void Update()
    {
        if ( Input.GetMouseButton(0))
        {
            Fire();
        }
    }


    private void Fire()
    {
        if (Time.time - _lastAttackTime > _attackTime)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (hit.collider != null)
                {
                    GameObject hitimpact = Instantiate(_hitImpact);
                    hitimpact.transform.position = ray.GetPoint(hit.distance);
                }
            }

            _coroutine = StartCoroutine(FlashEffect());
            _lastAttackTime = Time.time;

        }
    }

    private IEnumerator FlashEffect()
    {
        _fireFlash.SetActive(true);
        SoundManager.Instance.PlaySFX(_audioClip);

        yield return _waitForSeconds;

        _fireFlash.SetActive(false);
        _coroutine = null;

    }

}
