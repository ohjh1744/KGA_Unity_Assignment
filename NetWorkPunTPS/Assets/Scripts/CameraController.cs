using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviourPun
{
    [SerializeField] private float _mouseSensitive;

    [SerializeField] private float _yMinAxis;

    [SerializeField] private float _yMaxAxis;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Transform _target;

    public Transform Target { get { return _target; } set { _target = value; OnActiveCamera?.Invoke(); } }

    private UnityAction OnActiveCamera;

    private float _mouseX;

    private float _mouseY;

    private float _eulerAngleX;

    private float _eulerAngleY;

    private Quaternion _cameraRotation;

    private bool _isActive;

    private Vector3 _newVec;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnActiveCamera += ActiveCamera;
    }

    private void OnDisable()
    {
        OnActiveCamera -= ActiveCamera;
    }

    void Update()
    {
        if (_target == null)
        {
            return;
        }
        RotateCamera();
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        MoveCamera();
    }

    private void ActiveCamera()
    {
        transform.position = _target.position + _offset;
        _isActive = true;
    }

    private void RotateCamera()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        _cameraRotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

        transform.rotation = _cameraRotation;

        //회전한 만큼의 위치
        _newVec = _cameraRotation * _offset;
    }

    private void MoveCamera()
    {
        transform.position = _target.position + _newVec;

    }


}