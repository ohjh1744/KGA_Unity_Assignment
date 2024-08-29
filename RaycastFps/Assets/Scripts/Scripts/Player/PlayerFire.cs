using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private bool _isFire;
  
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _reLoadTime;
    [SerializeField]
    private float _fireRateTime;
    [SerializeField]
    private int _ammo;
    [SerializeField]
    private int _maxAmmo;

    private Coroutine _coroutineFire;
    private Coroutine _coroutineReLoad;
    private WaitForSeconds _coReLoadTime;
    private WaitForSeconds _coFireRateTime;


    private void Awake()
    {
        _coroutineFire = null;
        _coroutineReLoad = null;
        _coReLoadTime = new WaitForSeconds(_reLoadTime);
        _coFireRateTime = new WaitForSeconds(_fireRateTime);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            if(_coroutineFire == null)
            {
                _coroutineFire = StartCoroutine(Fire());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(_coroutineFire != null)
            {
                StopCoroutine(_coroutineFire);
                _coroutineFire = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(_coroutineReLoad == null)
            {
                _coroutineReLoad = StartCoroutine(ReLoad());
            }
        }

    }

    private IEnumerator Fire()
    {
        if(_ammo == 0)
        {
            Debug.Log("장전이 필요합니다!");
        }
        while (_ammo > 0)
        {
            Debug.Log("fire!");
            _ammo--;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Damagable")))
            {
                if (hit.collider != null)
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    damagable.GetDamage(_damage);
                }
            }
            yield return _coFireRateTime;
        }
    }

    private IEnumerator ReLoad()
    {
        Debug.Log("리로딩중");

        yield return _coReLoadTime;

        _ammo = _maxAmmo;
        Debug.Log("리로딩완료");

        _coroutineReLoad = null;
    }





}
