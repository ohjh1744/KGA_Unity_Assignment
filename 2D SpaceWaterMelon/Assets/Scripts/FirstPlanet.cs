using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlanet : Planet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == gameObject.tag)
        {
            ChangePlanet(collision);
        }
    }
    public override void ChangePlanet(Collision2D collision)
    {
        if (collision.gameObject.activeSelf == true)
        {
            collision.gameObject.SetActive(false);
            GameObject planet = PullManager.Instance.Get((int)PlanetType.두번째행성);
            planet.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }
}
