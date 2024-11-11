using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnChangeHp))] public int Hp { get; set; }

    public UnityAction<int> OnChangedHpEvent;

    public int MaxHp;

    public override void Spawned()
    {
        Hp = MaxHp;
    }

    private void OnChangeHp()
    {
        //TODO: UI반응 구현
        OnChangedHpEvent?.Invoke(Hp);
    }
}
