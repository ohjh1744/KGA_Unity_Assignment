using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Request(Permission.FineLocation);
    }

    public void Request(string targetPermission)
    {
        // 입력받은 종류의 권한이 이미 승인되어 있따면
        if (Permission.HasUserAuthorizedPermission(targetPermission))
        {
            OnSuccessed("Already Granted");
            return;
        }

        //승인되어 있지 않은 권한인 경우

        //권한 처리 결과에 대해서 (승인. 거절. 더이상 요청x) 시 반응에 대한 정보를 담는 객체
        PermissionCallbacks callbacks = new PermissionCallbacks();

        //승인 시 ,반응을 이벤트로 붙여서 구현
        callbacks.PermissionGranted += OnSuccessed;

        // 거부 시 반응을 이벤트로 붙여서 구현
        callbacks.PermissionDenied += OnDenied;

        Permission.RequestUserPermission(targetPermission, callbacks);
    }

    public void OnSuccessed(string str)
    {

    }

    public void OnDenied(string str)
    {

    }
}

