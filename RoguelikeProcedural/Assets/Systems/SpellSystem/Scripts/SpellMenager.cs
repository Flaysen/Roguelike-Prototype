using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpellMenager : MonoBehaviour{

    public Vector3 spellTarget;
    public PlayerController playerController;

    public Spell[] spellArray;
    public KeyCode[] castKey;
    public Transform[] turretArray;
    private float playerEnergy;

    // Use this for initialization
    void Awake()
    {
        castKey = new KeyCode[spellArray.Length];
        SetSpells();
    }

    // Update is called once per frame
    void Update()
    {
        CastSpell();
    }

    private void SetSpells()
    {
        int i = 0;
        foreach(Spell spell in spellArray)
        {
            if (spellArray[i] != null)
            {
                castKey[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (i + 1).ToString()); //Set Keys
                spell.nextCastTime = 0.0f; //Reset Cooldowns 
            }
            else castKey[i] = KeyCode.None;

            i++;
        }
    }

    private void CastSpell()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                if (castKey.Contains(vKey))
                {
                    int pos = Array.IndexOf(castKey, vKey);
                    Spell spellToCast = spellArray[pos];
                  
                    if (Time.time > spellToCast.nextCastTime &&
                        spellToCast.energy <= GetComponent<PlayerStats>().energy)
                    {
                        spellToCast.nextCastTime = Time.time + spellToCast.cooldown;
                        GetComponent<PlayerStats>().energy -= spellToCast.energy;
                        int t = 0; //turret index

                        if(spellToCast.castType == Spell.CastType.Projectile)                      
                        {
                            foreach (Transform transform in turretArray)
                            {
                                Instantiate(spellArray[pos], turretArray[t].position,
                                    turretArray[t].rotation);
                                t++;
                            }
                        }

                        if(spellToCast.castType == Spell.CastType.PointTarget)
                        {
                            spellTarget = playerController.pointToLook;
                            Instantiate(spellArray[pos], spellTarget, Quaternion.identity);
                        }

                        if (spellToCast.castType == Spell.CastType.SelfCast)
                        {
                            Instantiate(spellArray[pos], transform.position, Quaternion.identity);
                        }                     
                    }
                }
            }
        }
    }

}
