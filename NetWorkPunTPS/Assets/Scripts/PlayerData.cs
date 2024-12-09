using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int HP { get { return _hp; } set { _hp = value; } }

    [SerializeField] private Collider _colider;
    public Collider Coll { get { return _colider; } set { _colider = value; } }


}
