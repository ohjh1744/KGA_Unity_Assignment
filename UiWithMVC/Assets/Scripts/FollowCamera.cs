using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _weapon;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _mouseSensitive;

    private PlayerFire _playerFire; 
    private Quaternion _rotation;
    private RaycastHit _hit;
    private Vector3 _newOffset;
    private float _eulerAngleY;
    private float _eulerAngleX;
    private float _mouseX;
    private float _mouseY;
    public bool _isRotate;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        InputMousePos();
    }

    private void LateUpdate()
    {
        if (_isRotate)
        {
            RotateCamera();
        }
        else
        {
            FollowPlayer();
        }
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //카메라와 Player사이간 백터.
        _offset = transform.position - _player.position;
        // _offset값으로 초기화.
        _newOffset = _offset;
        _playerFire = _player.GetComponent<PlayerFire>();
    }

    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        //회전은 스페이스바로
        _isRotate = Input.GetKey(KeyCode.Space);
    }

    private void RotateCamera()
    {
        // 회전된 정도 구하기.
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;
        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -45f, 45f);
        _rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

        // 카메라와 Player간의 벡터방향을 회전된 정도 만큼 회전한 새로운 벡터.
        _newOffset = _rotation * _offset;

        FollowPlayer();

        // 카메라 회전.
        transform.rotation = _rotation;
        Quaternion weaponRotate = _rotation * Quaternion.Euler(0, 90, 0);
        _weapon.rotation = weaponRotate;

        // 플레이어는 왼쪽 오른쪽으로만 회전시키도록 x축과 y축 회전 0.
        _rotation.x = 0;
        _rotation.z = 0;
        _player.rotation = _rotation;
    }

    private void FollowPlayer()
    {
        if (_playerFire._isZoom == false)
        {
            // 카메라 위치 계산.
            Vector3 targetPosition = _player.position + _newOffset;

            // 플레이어와 카메라 사이에 장애물 존재 검사.
            if (Physics.Linecast(_player.position, targetPosition, out _hit))
            {
                // 벽이 있을 경우 카메라 위치를 벽과의 충돌 지점으로 한 뒤, 부드럽게 조정.
                transform.position = Vector3.Lerp(transform.position, _hit.point, 0.2f);
            }
            else
            {
                // 벽이 없을 경우 카메라를 목표 위치로 이동.
                transform.position = targetPosition;
            }
        }
    }
}