using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private GameObject _blade;
    [SerializeField] private GameObject _body;
    [Header("연료량")]
    [SerializeField] private float _energe = 100f;
    [Header("상하좌우이동속도")]
    [SerializeField] private float _moveSpeed;
    [Header("상승속도")]
    [SerializeField] private float _upSpeed;
    [Header("회전속도")]
    [SerializeField] private float _rotateSpeed;
    [Header("Blade회전")]
    [SerializeField] private float _rotateBladeRate;
    [SerializeField] private float _rotateBladeIncreaseRate = 2;
    [SerializeField] private float _maxRotateBladeRate = 10f;
    [SerializeField] private float _minRotateBladeRate = 0f;
    [Header("미사일")]
    [SerializeField] private Transform  _missilePos;
    [SerializeField] private GameObject _missile;
    [SerializeField] private float _attackTime;


    private float _energeTime = 0.1f;
    private float _lastEnergeTime;
    private float _lastAttackTime;
    private float _hMove;
    private float _vMove;
    private bool _isFly;
    private bool _isFire;
    private Transform _transform;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {
        ManageEnerge();
        Inputkey();
        Move();
        ReadyForFly();
        RotateBlade();
        RotateHead();
        Fly();
        Fire();
    }

    private void Inputkey()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        _isFly = Input.GetButton("Jump");
        _isFire = Input.GetKey(KeyCode.Keypad0);
    }

    private void Move()
    {
        _transform.Translate(new Vector3(_hMove, 0, _vMove) * _moveSpeed * Time.deltaTime, Space.Self);
    }


    private void ReadyForFly()
    {
        if (_isFly == true)
        {
            _rotateBladeRate += _rotateBladeIncreaseRate * Time.deltaTime;
            if (_rotateBladeRate > _maxRotateBladeRate)
            {
                _rotateBladeRate = _maxRotateBladeRate;
            }
        }
        else if (_isFly == false)
        {
            _rotateBladeRate -= _rotateBladeIncreaseRate * Time.deltaTime;
            if (_rotateBladeRate < _minRotateBladeRate)
            {
                _rotateBladeRate = _minRotateBladeRate;
            }

        }
    }

    private void Fly()
    {
        if (_rotateBladeRate > 5f && gameObject.transform.position.y < 50 && _energe > 0f)
        {
            if(Time.time - _lastEnergeTime > _energeTime)
            {
                _energe--;
                _lastEnergeTime = Time.time;
            }
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _transform.Translate(Vector3.up * _upSpeed * Time.deltaTime, Space.Self);
        }
        else if (_rotateBladeRate < 5f || _energe < 1f)
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }
    }

    private void ManageEnerge()
    {
        if(_energe > 100f)
        {
            _energe = 100f;
        }
        else if(_energe < 0f)
        {
            _energe = 0f;
        }
    }


    private void RotateBlade()
    {
        _blade.transform.Rotate(Vector3.up * _rotateBladeRate, Space.Self);
    }

    private void RotateHead()
    {
        Vector3 moveDirection = new Vector3(_hMove, 0, _vMove).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _body.transform.rotation = Quaternion.Slerp(_body.transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }

    private void Fire()
    {

        if (_isFire == true && Missile.NumMissile < 3)
        {
            if (Time.time - _lastAttackTime > _attackTime)
            {
                GameObject missile = Instantiate(_missile);
                missile.transform.position = _missilePos.position;
                missile.transform.rotation = _body.transform.rotation;
                _lastAttackTime = Time.time;
            }
        }

    }

}
