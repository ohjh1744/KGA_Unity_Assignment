using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lerp�� �ǵ��Ѵ�� �۵��� �ϰ��ִ��� �ñ��մϴ�. �����̰� �ȴ�������.?
// ��� ��ü�� �����ȰŰ�����, ���� �� ����� ����� ���� �� ������.. �� ����� ����� �˰��ͽ��ϴ�..!
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
        //ī�޶�� Player���̰� ����
        _offset = transform.position - _player.position;
        // _offset������ �ʱ�ȭ
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
        // ȸ���� ���� ���ϱ�
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;
        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -45f, 45f);
        _rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

        // ī�޶�� Player���� ���͹����� ȸ���� ���� ��ŭ ȸ���� ���ο� ����.
        _newOffset = _rotation * _offset;

        FollowPlayer();

        // ī�޶� ȸ��
        transform.rotation = _rotation;

        // �÷��̾�� ���� ���������θ� ȸ����Ű���� x��� y�� ȸ�� 0.
        _rotation.x = 0;
        _rotation.z = 0;
        _player.rotation = _rotation;
    }

    private void FollowPlayer()
    {
        // ī�޶� ��ġ ���.
        Vector3 targetPosition = _player.position + _newOffset;

        // �÷��̾�� ī�޶� ���̿� ��ֹ� ���� �˻�
        if (Physics.Linecast(_player.position, targetPosition, out _hit))
        {
            // ���� ���� ��� ī�޶� ��ġ�� ������ �浹 �������� �� ��, �ε巴�� ����.
            transform.position = Vector3.Lerp(transform.position, _hit.point, 0.2f);
        }
        else
        {
            // ���� ���� ��� ī�޶� ��ǥ ��ġ�� �̵�
            transform.position = targetPosition;
        }
    }
}