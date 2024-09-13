using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseZAngle;
    [SerializeField] private GameObject basicPlanet;
    [SerializeField] PlayerModel playerModel;
    [SerializeField] Slider slider;
    [SerializeField] private float maxPower;
    [SerializeField] private float  powerRange;
    [SerializeField] private float upPowerSpeed;
    private Vector3 mousePosition;
    private Vector2 dir;
    private WaitForSeconds upPowerSeconds;
    private Coroutine coroutine;


    void Awake()
    {
        upPowerSeconds = new WaitForSeconds(upPowerSpeed);
        coroutine = null;
    }

    private void OnEnable()
    {
        playerModel.UpdatePower += UpdatePower;
    }

    private void OnDisable()
    {
        playerModel.UpdatePower -= UpdatePower;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = mousePosition - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, baseZAngle + angle));

        if (Input.GetMouseButtonDown(0))
        {
            coroutine = StartCoroutine(UpPower());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(coroutine);
            coroutine = null;
            FIre();
        }

    }

    void FIre()
    {
        GameObject planet = PullManager.Instance.Get((int)PlanetType.±âº»);
        planet.transform.position = transform.position;
        Rigidbody2D rigidPlanet = planet.GetComponent<Rigidbody2D>();
        rigidPlanet.AddForce(dir * playerModel.Power, ForceMode2D.Impulse);
        playerModel.Power = 0;
    }

    IEnumerator UpPower()
    {
        while (true)
        {
            if(playerModel.Power > maxPower)
            {
                playerModel.Power = maxPower;
            }
            playerModel.Power += powerRange;

            yield return upPowerSeconds;
        }
    }

    void UpdatePower(float power )
    {
        slider.value = power / maxPower;
    }


}
