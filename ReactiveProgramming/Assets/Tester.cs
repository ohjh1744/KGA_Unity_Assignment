using DG.Tweening;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Update()
    {
        // ���� ���콺 ��ư�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0) == false)
            return;

        // ȭ����� ���콺 ��ġ�� �������� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ���̰� ���� ��ġ�� �̵�
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            MoveTo(hitInfo.point);
        }
    }

    public void MoveTo(Vector3 des)
    {
        Sequence sequence = DOTween.Sequence();

        // Append : ���� �ൿ�� �߰�
        // Prepend : ���� �ൿ�� �߰�
        // Join : �ش� �ൿ�� ���ÿ� ����
        // Insert(float, �޼���) : float�� �ڿ� �ش� �ൿ ���� (�ٸ� �ൿ�� ��ĥ ��� Join�� �����ϰ� ���� Ÿ�ֿ̹� ����)
        sequence
           .Append(transform.DOMove(des, 1f).SetEase(Ease.Linear))
           .Append(transform.DOShakeRotation(1f))
           .Insert(0, transform.DORotate(Vector3.zero, 0.1f));

        //transform.DOMove(des, 1f)
        //    .SetEase(Ease.InSine)
        //    .SetDelay(0.5f);
    }

}
