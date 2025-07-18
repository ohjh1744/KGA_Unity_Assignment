using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckCanAttackTarget", story: "Check [Player] Can Attack [Target]", category: "Action", id: "b7125065643ba557679b2ab30d50c225")]
public partial class CheckCanAttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    Collider2D[] _targets;

    protected override Status OnStart()
    {
        _targets = Physics2D.OverlapCircleAll(Player.Value.transform.position, 3f);

        Debug.Log(_targets.Length);
        //������ Ÿ���� ���ٸ�
        if (_targets.Length == 0)
        {
            //Target �ʱ�ȭ
            Target.Value = null;
            return Status.Failure;
        }
        else
        {
            return Status.Running;
        }
    }


    //������ Ÿ���� �ִ� ���. Update ����
    protected override Status OnUpdate()
    {

        GameObject nextTarget = _targets[_targets.Length - 1].gameObject;

        //������ Ÿ���� ���� ���ݹ������� �ִ��� üũ
        bool targetExists = Array.Exists(_targets, target => target.gameObject == Target.Value);

        // ������ ������ Target�� ���ݹ����� ����ų�, �׾ setfalse �Ǿ�����
        // Target ����
        if(targetExists == false || Target.Value.activeSelf == false)
        {
            Target.Value = nextTarget;
        }

        Debug.Log(Target.Value.gameObject.name);

        return Status.Success;
    }

}

