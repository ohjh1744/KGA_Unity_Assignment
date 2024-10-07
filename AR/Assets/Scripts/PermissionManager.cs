using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Android;
public class PermissionManager : MonoBehaviour
{
    [SerializeField] GPSManager gpsManager;

    private void Start()
    {
        Request(Permission.FineLocation);
    }

    public void Request(string targetPermission)
    {
        // 입력받은 종류의 권한이 이미 승인되어 있다면
        if (Permission.HasUserAuthorizedPermission(targetPermission))
        {
            OnSuccessed("Already Granted");
            return;
        }

        // 승인되어 있지 않은 권한인 경우

        // 권한 처리 결과에 대해서 (승인 / 거절 / 더이상 요청X) 시 반응에 대한 정보를 담는 객체
        PermissionCallbacks callbacks = new PermissionCallbacks();

        // 승인 시, 반응을 이벤트로 붙여서 구현
        callbacks.PermissionGranted += OnSuccessed;
        // 거부 시, 반응을 이벤트로 붙여서 구현
        callbacks.PermissionDenied += OnDenied;

        // 권한 요청 시도
        Permission.RequestUserPermission(targetPermission, callbacks);
    }

    public void OnSuccessed(string str)
    {
        // 권한을 성공적으로 승인받은 경우
        Debug.Log("GPS 권한 허용됨");
        gpsManager.GPSOn();
    }

    public void OnDenied(string str)
    {
        // 권한을 사용자가 거부하는 경우
        Debug.Log("GPS 권한 거부됨");
        gpsManager.GPSOff();
    }
}
