using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

public class UniRXTester : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    [SerializeField] private bool isGround;

    IDisposable jumpStream;
    private void Start()
    {
        jumpStream = this.UpdateAsObservable()        //������Ʈ���� ������ �������� ����
            .Where(x => Input.GetKeyDown(KeyCode.Space))  //���������� �����Ҷ� ���ǿ� �´� ��츸 �˷���
            .Where(x => isGround == true)
            .Select(x => transform.position)
            .Subscribe(x => Debug.Log($"������ Ȯ�ε� ��ġ : {x}")); //�������� �˷��ٶ����� ������ �Լ��� ��������.

        this.FixedUpdateAsObservable()
            .SampleFrame(10);   // 10�����Ӹ��� Ȯ���Ѵ�

        // ���� �ε����� Ground true
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // �̷��ĵ� ����
        //this.OnCollisionEnterAsObservable()
        //    .Where(collision =>collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //    .Subscribe(x => isGround = true);

        // ���� �������� isground false
        this.OnCollisionExitAsObservable()
             .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
             .Subscribe(x => isGround = false);

        // ��� 
        jumpStream.Dispose();
    }

    private void Jump()
    {
        rigid.velocity = Vector3.up * 5;
    }
}
