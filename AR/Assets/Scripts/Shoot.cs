using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float chargeTime;

    [SerializeField] private float powerGage;

    private float power;

    private Coroutine coroutine;

    private WaitForSeconds seconds;

    private StringBuilder sb;

    private void Awake()
    {
        seconds = new WaitForSeconds(chargeTime);
        sb = new StringBuilder();
    }

    public void StartChargePower()
    {
        coroutine = StartCoroutine(ChargePower());
    }

    IEnumerator ChargePower()
    {
        while (true)
        {
            Debug.Log(power);
            power += powerGage;
            sb.Clear();
            sb.Append(power);
            text.SetText(sb);
            yield return seconds;
        }
    }

    public void ShootBall()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        GameObject ball = Instantiate(ballPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.velocity = power * Camera.main.transform.forward;
        power = 0;
        sb.Clear();
        sb.Append(power);
        text.SetText(sb);
    }

}
