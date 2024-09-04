using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 _offset;
    private void LateUpdate()
    {
        transform.position = target.position + _offset;
    }
}
