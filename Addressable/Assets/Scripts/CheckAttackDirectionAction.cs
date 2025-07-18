using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Data.Common;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckAttackDirection", story: "Check [Player] Between [Target] [AttackDir]", category: "Action", id: "92c9cc7dfbb31f0f6c4a505eebed748c")]
public partial class CheckAttackDirectionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<attackDirection> AttackDir;

    private bool _isRight;

    protected override Status OnUpdate()
    {

        Vector2 attackDir = (Target.Value.transform.position - Player.Value.transform.position).normalized;
        // ���� ���
        float dot = Vector2.Dot(Vector2.up, attackDir);
        // ����, ������ ����
        // Target�� Player���� �����ʿ� ������ _isRight true
        if(Target.Value.transform.position.x - Player.Value.transform.position.x < 0)
        {
            _isRight = true;
        }
        else
        {
            _isRight = false;
        }

        
        // ���� ������ üũ
        if(dot > 0.707)
        {
            AttackDir.Value = attackDirection.attack12Dir;
            Debug.Log("���� �ִϸ��̼� ���� 12��");
        }
        else if (dot <= 0.707 && dot >= -0.707)
        {
            switch (_isRight)
            {
                case true:
                    AttackDir.Value = attackDirection.attack9Dir;
                    Debug.Log("���� �ִϸ��̼� ���� 9��");
                    break;
                case false:
                    AttackDir.Value = attackDirection.attack3Dir;
                    Debug.Log("���� �ִϸ��̼� ���� 3��");
                    break;

            }
        }
        else if(dot < -0.707)
        {
            AttackDir.Value = attackDirection.attack6Dir;
            Debug.Log("���� �ִϸ��̼� ���� 6��");
        }


        return Status.Success;
    }

}

