using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lerp가 의도한대로 작동을 하고있는지 궁금합니다. 별차이가 안느껴져서.?
// 기능 자체는 구현된거같지만, 무언가 더 깔끔한 방법이 있을 것 같은데.. 더 깔끔한 방법을 알고싶습니다..!
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _mouseSensitive;

    private Quaternion _rotation;
    private RaycastHit _hit;
    private Vector3 _newOffset;
    private float _eulerAngleY;
    private float _eulerAngleX;
    private float _mouseX;
    private float _mouseY;
    private bool _isRotate;

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
        //카메라와 Player사이간 백터
        _offset = transform.position - _player.position;
        // _offset값으로 초기화
        _newOffset = _offset;
    }

    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        _isRotate = Input.GetMouseButton(1);
    }

    private void RotateCamera()
    {
        // 회전된 정도 구하기
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;
        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -45f, 45f);
        _rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

        // 카메라와 Player간의 벡터방향을 회전된 정도 만큼 회전한 새로운 벡터.
        _newOffset = _rotation * _offset;

        FollowPlayer();

        // 카메라 회전
        transform.rotation = _rotation;

        // 플레이어는 왼쪽 오른쪽으로만 회전시키도록 x축과 y축 회전 0.
        _rotation.x = 0;
        _rotation.z = 0;
        _player.rotation = _rotation;
    }

    private void FollowPlayer()
    {
        // 카메라 위치 계산.
        Vector3 targetPosition = _player.position + _newOffset;

        // 플레이어와 카메라 사이에 장애물 존재 검사
        if (Physics.Linecast(_player.position, targetPosition, out _hit))
        {
            // 벽이 있을 경우 카메라 위치를 벽과의 충돌 지점으로 한 뒤, 부드럽게 조정.
            transform.position = Vector3.Lerp(transform.position, _hit.point, 0.2f);
        }
        else
        {
            // 벽이 없을 경우 카메라를 목표 위치로 이동
            transform.position = targetPosition;
        }
    }
}