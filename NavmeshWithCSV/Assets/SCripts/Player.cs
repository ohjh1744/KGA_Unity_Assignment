using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent player;
    Vector3 targetPosition;
    public int Count;
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        targetPosition = transform.position;
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPosition = hit.point;
            }
        }
        Move();


    }

    private void Move()
    {
        player.destination = targetPosition;
    }
}
