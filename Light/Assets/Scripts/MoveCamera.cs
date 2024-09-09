using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]private float _mouseSensitive; // 마우스 민감도
    [SerializeField] private float _yMinAxis;
    [SerializeField] private float _yMaxAxis;


    private float _eulerAngleY;
    private float _eulerAngleX;
    private float _mouseX;
    private float _mouseY;


    private void Awake()
    {
        Initialize();
    }

    private void Update()
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
        // 마우스 입력에 따라 카메라 회전
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        Quaternion rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);
        transform.rotation = rotation;

    }

}

