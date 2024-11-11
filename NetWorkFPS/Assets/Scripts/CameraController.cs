using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitive; // 마우스 민감도

    [SerializeField] private float _yMinAxis;

    [SerializeField] private float _yMaxAxis;

    [SerializeField] private float _offset;

    [SerializeField] private Transform _target;

    public Transform Target { get { return _target; } set { _target = value; } }

    private float _mouseX;

    private float _mouseY;

    private float _eulerAngleX;

    private float _eulerAngleY;

    private Quaternion _cameraRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        RotateCamera();
    }

    private void RotateCamera()
    {
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        _cameraRotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

        transform.rotation = _cameraRotation;
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = _target.position + (_target.forward * _offset);
    }
}
