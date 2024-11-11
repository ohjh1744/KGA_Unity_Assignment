using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : NetworkBehaviour
{
    // �ٲ������ OnChangedRender ȣ��
    //Networked�� ��Ʈ��ũ���� ����ȭ�� �����͸� ����. �����ĸ� ����. ������Ƽ �ʼ�, �ν�����â���� ���̵��� ����.
    //OnChangedRender:��Ʈ��ũ �����Ͱ� ����Ǿ��� �� ȣ��Ǵ� �Լ��� ����.
    [Networked, OnChangedRender(nameof(OnChangeHp))] public int Hp { get;  set; }

    public UnityAction<int> OnChangedHpEvent;

    public int MaxHp;

    public override void Spawned()
    {
        Hp = MaxHp;
    }

    private void OnChangeHp()
    {
        //TODO: UI���� ����
        OnChangedHpEvent?.Invoke(Hp);
    }
}
