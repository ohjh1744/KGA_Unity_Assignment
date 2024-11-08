using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;

    public Vector3 _offset;

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = _target.position + _offset;
        transform.LookAt(_target.position);
    }
}
