using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]private float _mouseSensitive; // ���콺 �ΰ���
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

    // ���콺 ��ġ �Է�
    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    // ī�޶� ȸ��
    private void RotateCamera()
    {
        // ���콺 �Է¿� ���� ī�޶� ȸ��
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        Quaternion rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);
        transform.rotation = rotation;

    }

}
