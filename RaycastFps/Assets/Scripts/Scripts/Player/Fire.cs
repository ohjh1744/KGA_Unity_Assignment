using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private bool _isFire;
  
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private int _damage;

    void Update()
    {
        _isFire = Input.GetMouseButtonDown(0);
        OnFire();
    }

    private void OnFire()
    {
        if (_isFire == true)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Damagable")))
            {
                if (hit.collider != null)
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    damagable.GetDamage(_damage);
                }
            }
        }

    }

}
