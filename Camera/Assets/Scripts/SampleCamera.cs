using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SampleCamera : MonoBehaviour
{
    [SerializeField] float rate;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float yAngle;
    [SerializeField] float xAngle;
    [SerializeField] float distance;
    [SerializeField] Transform target;

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            return;
        }

        xAngle -= Input.GetAxis("Mouse Y");
        yAngle += Input.GetAxis("Mouse X");
    }

    private void Move()
    {
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
    }
}
