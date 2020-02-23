using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{   
    public enum CastType
    {
        Projectile,
        PointTarget,
        SelfCast
    }

    [Header("CastType")]

    public CastType castType;

    [Header("COSTS")]

    public int energy = 0;

    [Range(0.0f, 20.0f)] public float cooldown;

    public float nextCastTime;

    protected bool CheckIfMonster(GameObject other)
    {
        Monster monster = other?.GetComponent<Monster>();
        return (monster != null) ? true : false;
        
    }

}
