using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    private float _yRotate;
    private float _xRotate;
    private float _eulerAngleY;
    private float _eulerAngleX;
    [SerializeField] private float _rotateHeadSpeed;

 
    void Update()
    {
        //프로젝트 세팅에서 vertical과 horizontal의 a, d, s, w는 제거.
        _yRotate = Input.GetAxis("Vertical");
        _xRotate = Input.GetAxis("Horizontal");
        OnRotateHead();
    }

    private void OnRotateHead()
    {
        _eulerAngleY += _xRotate * _rotateHeadSpeed;
        _eulerAngleX -= _yRotate * _rotateHeadSpeed;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -60f, 0f);

        transform.rotation = Quaternion.Euler(_eulerAngleX, _eulerAngleY, 0);
    }
}
