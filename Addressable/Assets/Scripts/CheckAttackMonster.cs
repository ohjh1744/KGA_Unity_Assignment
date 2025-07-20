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

            //���� ������ Ÿ���� ���ٸ� �������� Ÿ���� ����Ÿ������ �����ϰ�, ��������Ÿ�� �ʱ�ȭ
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
        // ����Ÿ���� ���ݹ��������� ����ٸ� 
        if (collision.gameObject == _currentTarget)
        {
            ChangeNextTargetToCurrentTarget();
        }
    }

    private void Update()
    {
        //���� Ÿ���� �׾ false�� �Ǿ��ٸ�
        if (_currentTarget != null && _currentTarget.gameObject.activeSelf == false)
        {
            ChangeNextTargetToCurrentTarget();
        }
    }

    private void ChangeNextTargetToCurrentTarget()
    {
        //������ Ÿ�� �ʱ�ȭ
        _currentTarget = null;
        _behaviorAgent.SetVariableValue("currentTarget", _currentTarget);
        _behaviorAgent.SetVariableValue("isFindTarget", false);

        // ���� ����Ÿ���� �ִٸ� ���綧�� Ÿ������ ����
        if (_nextTarget != null)
        {
            _currentTarget = _nextTarget.gameObject;
            _nextTarget = null;
            _behaviorAgent.SetVariableValue("currentTarget", _currentTarget);
            _behaviorAgent.SetVariableValue("isFindTarget", true);
        }
    }
}
