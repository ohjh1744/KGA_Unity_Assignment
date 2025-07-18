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
        // 내적 계산
        float dot = Vector2.Dot(Vector2.up, attackDir);
        // 왼쪽, 오른쪽 구별
        // Target이 Player보다 오른쪽에 있으면 _isRight true
        if(Target.Value.transform.position.x - Player.Value.transform.position.x < 0)
        {
            _isRight = true;
        }
        else
        {
            _isRight = false;
        }

        
        // 내적 각도별 체크
        if(dot > 0.707)
        {
            AttackDir.Value = attackDirection.attack12Dir;
            Debug.Log("공격 애니메이션 방향 12시");
        }
        else if (dot <= 0.707 && dot >= -0.707)
        {
            switch (_isRight)
            {
                case true:
                    AttackDir.Value = attackDirection.attack9Dir;
                    Debug.Log("공격 애니메이션 방향 9시");
                    break;
                case false:
                    AttackDir.Value = attackDirection.attack3Dir;
                    Debug.Log("공격 애니메이션 방향 3시");
                    break;

            }
        }
        else if(dot < -0.707)
        {
            AttackDir.Value = attackDirection.attack6Dir;
            Debug.Log("공격 애니메이션 방향 6시");
        }


        return Status.Success;
    }

}

