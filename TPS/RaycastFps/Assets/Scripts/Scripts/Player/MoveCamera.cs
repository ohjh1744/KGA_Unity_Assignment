using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private float _mouseSensitive;
    private float _eulerAngleY;
    private float _eulerAngleX;
    private float _mouseX;
    private float _mouseY;

    private void Awake()
    {
        Initialize();
    }

    private void LateUpdate()
    {
        InputMousePos();
        RotateCamera();
    }
    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


    // 마우스 위치 입력
    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    // 카메라 회전
    private void RotateCamera()
    {
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -25f, 25f);

        transform.rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);
    }
}
