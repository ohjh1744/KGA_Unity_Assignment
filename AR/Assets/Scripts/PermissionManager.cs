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
        // �Է¹��� ������ ������ �̹� ���εǾ� �ֵ���
        if (Permission.HasUserAuthorizedPermission(targetPermission))
        {
            OnSuccessed("Already Granted");
            return;
        }

        //���εǾ� ���� ���� ������ ���

        //���� ó�� ����� ���ؼ� (����. ����. ���̻� ��ûx) �� ������ ���� ������ ��� ��ü
        PermissionCallbacks callbacks = new PermissionCallbacks();

        //���� �� ,������ �̺�Ʈ�� �ٿ��� ����
        callbacks.PermissionGranted += OnSuccessed;

        // �ź� �� ������ �̺�Ʈ�� �ٿ��� ����
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

