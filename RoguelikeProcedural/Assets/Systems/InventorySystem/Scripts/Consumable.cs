using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    [SerializeField] private int rehabValue = 20;

    public override void OnUse()
    {
        base.OnUse();

        stats.health = (stats.health + rehabValue < stats.MaxHealth.Value) ?
            stats.health + rehabValue : stats.MaxHealth.Value;
    }
}
