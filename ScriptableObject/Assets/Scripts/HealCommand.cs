using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Heal")]
public class HealCommand : Command
{
    public override void Excute()
    {
        Debug.Log("ÈúÇÏ±â");
    }
}
