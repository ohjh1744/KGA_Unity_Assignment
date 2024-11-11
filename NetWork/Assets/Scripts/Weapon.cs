using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEditor;

public class Weapon : NetworkBehaviour
{
    [SerializeField] Transform _muzzlePoint;

    [SerializeField] private int _damage;
    [Range(0, 20)]
    [SerializeField] private float _range;

    public void FIre()
    {
        if(Runner.GetPhysicsScene().Raycast(_muzzlePoint.position, _muzzlePoint.forward, out RaycastHit info, _range))
        {
            if(info.transform.tag != "Player")
            {
                return;
            }
            Debug.Log(info.transform.name);

            PlayerController player = info.transform.GetComponent<PlayerController>();
            player.TakeHitRpc(_damage);
        }
    }




}
