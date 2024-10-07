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
        // �Է¹��� ������ ������ �̹� ���εǾ� �ִٸ�
        if (Permission.HasUserAuthorizedPermission(targetPermission))
        {
            OnSuccessed("Already Granted");
            return;
        }

        // ���εǾ� ���� ���� ������ ���

        // ���� ó�� ����� ���ؼ� (���� / ���� / ���̻� ��ûX) �� ������ ���� ������ ��� ��ü
        PermissionCallbacks callbacks = new PermissionCallbacks();

        // ���� ��, ������ �̺�Ʈ�� �ٿ��� ����
        callbacks.PermissionGranted += OnSuccessed;
        // �ź� ��, ������ �̺�Ʈ�� �ٿ��� ����
        callbacks.PermissionDenied += OnDenied;

        // ���� ��û �õ�
        Permission.RequestUserPermission(targetPermission, callbacks);
    }

    public void OnSuccessed(string str)
    {
        // ������ ���������� ���ι��� ���
        Debug.Log("GPS ���� ����");
        gpsManager.GPSOn();
    }

    public void OnDenied(string str)
    {
        // ������ ����ڰ� �ź��ϴ� ���
        Debug.Log("GPS ���� �źε�");
        gpsManager.GPSOff();
    }
}
