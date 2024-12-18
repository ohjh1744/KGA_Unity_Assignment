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
    // 줌들어가는 속도
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private GameObject WeaponCamera;
    [SerializeField] private GameObject Weapon;

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
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isZoom = false;
            _aim.SetActive(false);
            Weapon.SetActive(true);
            WeaponCamera.SetActive(false);
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
        //Lerp 때문에 줌상태에서 카메라가 따라오는게 약간 텀이 있어서 움직일때 Player나 총이 늦게 따라오는 현상이 발생
        //이에 서브카메라를 둠.
        // 만약 서브카메라 없이 하고 싶다면 Lerp를 없애면댐.
        _camera.transform.position = Vector3.Lerp(_originCamerapos, _zoomPos.position, _zoomSpeed * Time.deltaTime);
        Weapon.SetActive(false);
        WeaponCamera.SetActive(true);

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
