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

        // 1. 디바이스가 GPS를 사용중이지 않는다면 종료
        if (Input.location.isEnabledByUser == false)
            yield break;

        // 2. GPS를 시작한다.
        Input.location.Start();
        Debug.Log("GPS 연결 시작");

        // 3. GPS가 준비될 때까지 대기
        int waitCount = 20;
        while (true)
        {
            // 3-1. GPS가 준비중인 상황일 때
            if (Input.location.status == LocationServiceStatus.Initializing)
            {
                Debug.Log($"GPS 연결 대기 {waitCount}");
                // 잠시 기다렸다가
                yield return delay;
                waitCount--;

                // 20회 이상 준비중일 때는 => GPS가 정상적이지 않은 상황으로 판단
                if (waitCount <= 0)
                {
                    Debug.Log($"GPS 연결 실패");
                    Input.location.Stop();
                    gpsRoutine = null;
                    yield break;
                }
            }
            // 3-2. GPS 연결에 실패한 상황일 때
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log($"GPS 연결 실패");
                Input.location.Stop();
                gpsRoutine = null;
                yield break;
            }
            // 3-3. GPS 연결에 성공한 상황일 때
            else if (Input.location.status == LocationServiceStatus.Running)
            {
                Debug.Log($"GPS 연결 성공");
                break;
            }
        }

        // 4. 위 단계들을 통과했을 경우 (정상 구동 확인 시) 이정 주기로 GPS 값을 받아오도록 함
        while (true)
        {
            LocationInfo locationInfo = Input.location.lastData;
            Debug.Log($"위도 : {locationInfo.latitude} / 경도 : {locationInfo.longitude}");
            yield return delay;
        }
    }
}
