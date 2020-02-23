using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : Item
{
    public Vector3 spellTarget;
    public Spell spellToCast;
    public PlayerController playerController;
    [SerializeField] private Transform turret;

    public override void OnUse()
    {
        CastSpell();
    }

    public void CastSpell()
    {
        switch (spellToCast.castType)
        {
            case Spell.CastType.PointTarget:
                spellTarget = playerController.pointToLook;
                Instantiate(spellToCast, spellTarget, Quaternion.identity);
                break;
            case Spell.CastType.Projectile:
                spellTarget = turret.transform.position;
                Instantiate(spellToCast, spellTarget, Quaternion.identity);
                break;
            case Spell.CastType.SelfCast:
                spellTarget = playerController.transform.position;
                Instantiate(spellToCast, spellTarget, Quaternion.identity);
                break;
            default:
                Debug.Log("Spell target error");
                break;
        }       
    }
}
