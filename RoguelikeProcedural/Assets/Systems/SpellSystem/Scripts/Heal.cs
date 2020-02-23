using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Spell
{

    [SerializeField] private PlayerStats stats;

    void Start()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        stats.MaxHealth.AddModifier(new StatModifier(10, StatModType.Flat));
    }
}
