using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    private float power;

    public float Power { get { return power; } set { power = value; UpdatePower?.Invoke(power); } }

    public UnityAction<float> UpdatePower;

}
