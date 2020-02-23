using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplifier : Equipment
{
    public override void OnPickup()
    {
        base.OnPickup();
        Equip();
    }
}
