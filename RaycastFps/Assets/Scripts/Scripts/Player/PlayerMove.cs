using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigid;
    private Vector3 _moveDir;
    private float _hMove;
    private float _vMove;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        Move();
    }


    private void Move()
    {
        _moveDir = new Vector3(_hMove, 0, _vMove);
        Vector3 dir = transform.rotation * _moveDir * _speed;
        dir.y = 0f;
        _rigid.velocity = dir;
    }
}
