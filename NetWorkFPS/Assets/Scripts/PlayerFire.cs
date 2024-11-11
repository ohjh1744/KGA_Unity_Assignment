using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : NetworkBehaviour
{
    [SerializeField] private float _fireDistance;

    [SerializeField] private int _damage;

    [SerializeField] private int _curAmmo;

    [SerializeField] private int _maxAmmo;

    public int CurAmmo { get { return _curAmmo; } set { _curAmmo = value; } }

    public void Fire()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Runner.GetPhysicsScene().Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit info, _fireDistance))
        {
            if (info.transform.tag != "Player")
            {
                return;
            }
            Debug.Log(info.transform.name);

            PlayerController player = info.transform.GetComponent<PlayerController>();
            player.TakeHitRpc(_damage);
        }
    }

    public void ReLoad()
    {
        _curAmmo = _maxAmmo;
    }
}
