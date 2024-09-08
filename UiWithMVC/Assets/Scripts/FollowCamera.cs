using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offsetDistance;
    [SerializeField] private float _mouseSensitive;
    [SerializeField] private PlayerFire _playerFire;
    [SerializeField] private float _cameraTurnSpeed;

    private float _xMousePos;
    private float _yMousePos;
    private float _eulerAngleX;
    private float _eulerAngleY;
 
    public Quaternion _mouseRotation;
    private Vector3 _cameraVec;



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
            _xMousePos = Input.GetAxis("Mouse X");
            _yMousePos = Input.GetAxis("Mouse Y");

            _eulerAngleX -= _yMousePos * _mouseSensitive;
            _eulerAngleY += _xMousePos * _mouseSensitive;
            _mouseRotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);

            transform.rotation = _mouseRotation;
            _cameraVec = _mouseRotation * _offsetDistance;
        }

        if (!_playerFire._isZoom)
        {
            transform.position = _player.position + _cameraVec;
        }


    }


}
