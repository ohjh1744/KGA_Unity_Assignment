using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _xSpeed;

    void Start()
    {
        _camera.m_YAxis.m_MaxSpeed = 0;
        _camera.m_XAxis.m_MaxSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            _camera.m_YAxis.m_MaxSpeed = _ySpeed;
            _camera.m_XAxis.m_MaxSpeed = _xSpeed;
        }
        else
        {
            _camera.m_YAxis.m_MaxSpeed = 0;
            _camera.m_XAxis.m_MaxSpeed = 0;
        }
    }
}
