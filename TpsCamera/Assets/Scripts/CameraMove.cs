using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offsetDistance;
    [SerializeField] private float _mouseSensitive;

    private float _xMousePos;
    private float _yMousePos;
    private float _eulerAngleX;
    private float _eulerAngleY;

    public Quaternion _mouseRotation;
    private Vector3 _cameraVec;

    //마우스 회전에 따른 Player가 바라봐야하는 방향
    private Quaternion playerDir;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        _cameraVec = _offsetDistance;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 playerRayDir = ray.direction;
            playerRayDir.y = 0;
            playerDir = Quaternion.LookRotation(playerRayDir);

            _xMousePos = Input.GetAxis("Mouse X");
            _yMousePos = Input.GetAxis("Mouse Y");

            _eulerAngleX -= _yMousePos * _mouseSensitive;
            _eulerAngleY += _xMousePos * _mouseSensitive;
            _mouseRotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

            transform.rotation = _mouseRotation;
            _cameraVec = _mouseRotation * _offsetDistance;
        }

        transform.position = _player.position + _cameraVec;


    }
}
