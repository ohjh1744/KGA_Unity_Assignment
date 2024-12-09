using DG.Tweening;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Update()
    {
        // 왼쪽 마우스 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0) == false)
            return;

        // 화면상의 마우스 위치를 기준으로 레이
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 레이가 닿은 위치로 이동
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            MoveTo(hitInfo.point);
        }
    }

    public void MoveTo(Vector3 des)
    {
        Sequence sequence = DOTween.Sequence();

        // Append : 다음 행동에 추가
        // Prepend : 이전 행동에 추가
        // Join : 해당 행동과 동시에 진행
        // Insert(float, 메서드) : float초 뒤에 해당 행동 진행 (다른 행동과 겹칠 경우 Join과 동일하게 같은 타이밍에 진행)
        sequence
           .Append(transform.DOMove(des, 1f).SetEase(Ease.Linear))
           .Append(transform.DOShakeRotation(1f))
           .Insert(0, transform.DORotate(Vector3.zero, 0.1f));

        //transform.DOMove(des, 1f)
        //    .SetEase(Ease.InSine)
        //    .SetDelay(0.5f);
    }

}
