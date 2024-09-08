using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _camera;
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _xSpeed;
    [SerializeField] private Vector3 _offsetDistance;


    public Quaternion PlayerRotation;
    void Start()
    {
        _camera.m_YAxis.m_MaxSpeed = 0;
        _camera.m_XAxis.m_MaxSpeed = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            _camera.m_YAxis.m_MaxSpeed = _ySpeed;
            _camera.m_XAxis.m_MaxSpeed = _xSpeed;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 playerRayDir = ray.direction;
            playerRayDir.y = 0;
            PlayerRotation = Quaternion.LookRotation(playerRayDir);
        }
        else
        {
            _camera.m_YAxis.m_MaxSpeed = 0;
            _camera.m_XAxis.m_MaxSpeed = 0;
        }
        Debug.Log(PlayerRotation);
    }
}
