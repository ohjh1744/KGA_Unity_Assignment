using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : NetworkBehaviour
{
    // 바뀌었을때 OnChangedRender 호출
    //Networked는 네트워크에서 동기화할 데이터를 선정. 값형식만 가능. 프로퍼티 필수, 인스펙터창에서 보이도록 가능.
    //OnChangedRender:네트워크 데이터가 변경되었을 때 호출되는 함수를 지정.
    [Networked, OnChangedRender(nameof(OnChangeHp))] public int Hp { get;  set; }

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
