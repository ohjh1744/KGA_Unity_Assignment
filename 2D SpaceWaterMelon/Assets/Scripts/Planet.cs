using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetType {�⺻, ù��°�༺, �ι�°�༺, ����°�༺}
public abstract class Planet : MonoBehaviour
{
    public abstract void ChangePlanet(Collision2D collision);
}
