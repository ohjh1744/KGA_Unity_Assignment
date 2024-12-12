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
        jumpStream = this.UpdateAsObservable()        //업데이트마다 반응할 옵저버를 만듬
            .Where(x => Input.GetKeyDown(KeyCode.Space))  //옵저버에게 반응할때 조건에 맞는 경우만 알려줌
            .Where(x => isGround == true)
            .Select(x => transform.position)
            .Subscribe(x => Debug.Log($"점프가 확인됨 위치 : {x}")); //옵저버가 알려줄때마다 실행할 함수를 연결해줌.

        this.FixedUpdateAsObservable()
            .SampleFrame(10);   // 10프레임마다 확인한다

        // 땅과 부딪힐때 Ground true
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // 이런식도 가능
        //this.OnCollisionEnterAsObservable()
        //    .Where(collision =>collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //    .Subscribe(x => isGround = true);

        // 땅과 떨어질때 isground false
        this.OnCollisionExitAsObservable()
             .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
             .Subscribe(x => isGround = false);

        // 취소 
        jumpStream.Dispose();
    }

    private void Jump()
    {
        rigid.velocity = Vector3.up * 5;
    }
}
