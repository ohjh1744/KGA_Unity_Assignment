using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : MonoBehaviour
{
    [SerializeField] private Transform _throwPos;
    [SerializeField]private GameObject _explodeItem;
    [SerializeField] private float _power;
    private Grenade _grenade;
    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Throw();
        }
    }

    private void Throw()
    {

        GameObject grenade = Instantiate(_explodeItem);
        grenade.transform.position = _throwPos.position;
        grenade.transform.rotation = _throwPos.transform.rotation;
        grenade.SetActive(true);

        Rigidbody grenadeRigid = grenade.GetComponent<Rigidbody>();
        grenadeRigid.AddForce(grenadeRigid.transform.forward * _power, ForceMode.Impulse);

    }

}
