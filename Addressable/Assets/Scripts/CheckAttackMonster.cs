using Unity.Behavior;
using UnityEngine;

public class CheckAttackMonster : MonoBehaviour
{
    [SerializeField] private BehaviorGraphAgent _behaviorAgent;

    [SerializeField] private GameObject _currentTarget;

    [SerializeField] private GameObject _nextTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            _nextTarget = collision.gameObject;

            //현재 때리는 타겟이 없다면 다음때릴 타겟을 현재타겟으로 변경하고, 다음때릴타겟 초기화
            if (_currentTarget == null)
            {
                _currentTarget = _nextTarget;
                _behaviorAgent.SetVariableValue("currentTarget", _currentTarget);
                _behaviorAgent.SetVariableValue("isFindTarget", true);
                _nextTarget = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 현재타겟이 공격범위밖으로 벗어난다면 
        if (collision.gameObject == _currentTarget)
        {
            ChangeNextTargetToCurrentTarget();
        }
    }

    private void Update()
    {
        //현재 타겟이 죽어서 false가 되었다면
        if (_currentTarget != null && _currentTarget.gameObject.activeSelf == false)
        {
            ChangeNextTargetToCurrentTarget();
        }
    }

    private void ChangeNextTargetToCurrentTarget()
    {
        //때리는 타겟 초기화
        _currentTarget = null;
        _behaviorAgent.SetVariableValue("currentTarget", _currentTarget);
        _behaviorAgent.SetVariableValue("isFindTarget", false);

        // 다음 때릴타겟이 있다면 현재때릴 타겟으로 변경
        if (_nextTarget != null)
        {
            _currentTarget = _nextTarget.gameObject;
            _nextTarget = null;
            _behaviorAgent.SetVariableValue("currentTarget", _currentTarget);
            _behaviorAgent.SetVariableValue("isFindTarget", true);
        }
    }
}
