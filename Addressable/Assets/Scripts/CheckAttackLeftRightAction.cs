using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckAttackLeftRight", story: "Check [Player] [isAttackRight] [Target]", category: "Action", id: "365cbbb01606ef84fab2cb8de948ca72")]
public partial class CheckAttackLeftRightAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<bool> IsAttackRight;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        Debug.Log("start!!!!");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Target.Value.transform.position.x < Player.Value.transform.position.x )
        {
            Debug.Log("¿ÞÂÊ!");
            IsAttackRight.Value = false;
        }
        else
        {
            Debug.Log("¿À¸¥ÂÊ!");
            IsAttackRight.Value = true;
        }

        return Status.Success;
    }

}

