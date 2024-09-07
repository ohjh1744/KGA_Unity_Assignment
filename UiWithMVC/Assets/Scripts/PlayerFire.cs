using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text _curBulletText;
    [SerializeField]private Text _maxBulletText;
    [SerializeField] private GameObject _aim;

    [Header("Model")]
    [SerializeField]private PlayerModel _playerModel;


    [SerializeField]private Camera _camera;
    [SerializeField]private Transform _zoomPos;
    [SerializeField]private float _fireRateTime;
    // 줌시에 바라보는 방향으로 돌아가는 회전속도
    [SerializeField] private float _zoomPlyaerRotateRate;

    private Coroutine _coroutineFire;
    private WaitForSeconds _coFireRateTime;
    private Vector3 _originCamerapos;
    public bool _isZoom;



    private void Awake()
    {
        _coroutineFire = null;
        _coFireRateTime = new WaitForSeconds(_fireRateTime);
    }

    private void Start()
    {
        UpdateBullet(_playerModel.Bullet);
        UpdateMaxBullet(_playerModel.MaxBullet);
    }

    private void OnEnable()
    {
        _playerModel.OnBulletChanged += UpdateBullet;
        _playerModel.OnMaxBulletChanged += UpdateMaxBullet;
    }

    private void OnDisable()
    {
        _playerModel.OnBulletChanged -= UpdateBullet;
        _playerModel.OnMaxBulletChanged -= UpdateMaxBullet;
    }


    void Update()
    {
        Debug.DrawRay(_camera.ScreenPointToRay(Input.mousePosition).origin, _camera.ScreenPointToRay(Input.mousePosition).direction * 100, Color.red);
        if (Input.GetMouseButtonDown(0))
        {

            if (_coroutineFire == null)
            {
                _coroutineFire = StartCoroutine(Fire());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_coroutineFire != null)
            {
                StopCoroutine(_coroutineFire);
                _coroutineFire = null;
            }
        }
        if (Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isZoom = false;
            _aim.SetActive(false);
        }
    }

    private IEnumerator Fire()
    {
        while (_playerModel.Bullet > 0)
        {
            Debug.Log("fire!");
            _playerModel.Bullet--;
        

            yield return _coFireRateTime;
        }
    }

    void ZoomIn()
    {
        _isZoom = true;
        _originCamerapos = _camera.transform.position;
        _aim.SetActive(true);
        _camera.transform.position = Vector3.Lerp(_originCamerapos, _zoomPos.position, 0.02f);

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Quaternion playerDir = Quaternion.LookRotation(ray.direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerDir, _zoomPlyaerRotateRate * Time.deltaTime);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Damagable")))
        {
            if (hit.collider != null)
            {
                MonsterController monsterController;
                monsterController = hit.transform.GetComponent<MonsterController>();
                monsterController.Canvas.SetActive(true);
                monsterController.Canvas.transform.forward = -(ray.direction);
            }
        }


    }


    void UpdateBullet(int bullet)
    {
        _curBulletText.text = $"{bullet} / ";
    }

    void UpdateMaxBullet(int maxbullet)
    {
        _maxBulletText.text = $"{maxbullet}";
    }



}
