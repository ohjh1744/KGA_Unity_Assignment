using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetType {기본, 첫번째행성, 두번째행성, 세번째행성}
public abstract class Planet : MonoBehaviour
{
    public abstract void ChangePlanet(Collision2D collision);
}
