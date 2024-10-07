using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GPSManager : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;

    Coroutine gpsRoutine;
    public void GPSOn()
    {
        if (gpsRoutine != null)
            return;

        gpsRoutine = StartCoroutine(GPSRoutine());
    }

    public void GPSOff()
    {
        if (gpsRoutine == null)
            return;

        StopCoroutine(gpsRoutine);
        gpsRoutine = null;
    }

    IEnumerator GPSRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(delayTime);

        // 1. ����̽��� GPS�� ��������� �ʴ´ٸ� ����
        if (Input.location.isEnabledByUser == false)
            yield break;

        // 2. GPS�� �����Ѵ�.
        Input.location.Start();
        Debug.Log("GPS ���� ����");

        // 3. GPS�� �غ�� ������ ���
        int waitCount = 20;
        while (true)
        {
            // 3-1. GPS�� �غ����� ��Ȳ�� ��
            if (Input.location.status == LocationServiceStatus.Initializing)
            {
                Debug.Log($"GPS ���� ��� {waitCount}");
                // ��� ��ٷȴٰ�
                yield return delay;
                waitCount--;

                // 20ȸ �̻� �غ����� ���� => GPS�� ���������� ���� ��Ȳ���� �Ǵ�
                if (waitCount <= 0)
                {
                    Debug.Log($"GPS ���� ����");
                    Input.location.Stop();
                    gpsRoutine = null;
                    yield break;
                }
            }
            // 3-2. GPS ���ῡ ������ ��Ȳ�� ��
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log($"GPS ���� ����");
                Input.location.Stop();
                gpsRoutine = null;
                yield break;
            }
            // 3-3. GPS ���ῡ ������ ��Ȳ�� ��
            else if (Input.location.status == LocationServiceStatus.Running)
            {
                Debug.Log($"GPS ���� ����");
                break;
            }
        }

        // 4. �� �ܰ���� ������� ��� (���� ���� Ȯ�� ��) ���� �ֱ�� GPS ���� �޾ƿ����� ��
        while (true)
        {
            LocationInfo locationInfo = Input.location.lastData;
            Debug.Log($"���� : {locationInfo.latitude} / �浵 : {locationInfo.longitude}");
            yield return delay;
        }
    }
}
