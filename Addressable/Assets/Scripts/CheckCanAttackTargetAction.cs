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
        //공격할 타겟이 없다면
        if (_targets.Length == 0)
        {
            //Target 초기화
            Target.Value = null;
            return Status.Failure;
        }
        else
        {
            return Status.Running;
        }
    }


    //공격할 타겟이 있는 경우. Update 진행
    protected override Status OnUpdate()
    {

        GameObject nextTarget = _targets[_targets.Length - 1].gameObject;

        //기존의 타겟이 아직 공격범위내에 있는지 체크
        bool targetExists = Array.Exists(_targets, target => target.gameObject == Target.Value);

        // 기존에 지정된 Target이 공격범위를 벗어나거나, 죽어서 setfalse 되었따면
        // Target 변경
        if(targetExists == false || Target.Value.activeSelf == false)
        {
            Target.Value = nextTarget;
        }

        Debug.Log(Target.Value.gameObject.name);

        return Status.Success;
    }

}

