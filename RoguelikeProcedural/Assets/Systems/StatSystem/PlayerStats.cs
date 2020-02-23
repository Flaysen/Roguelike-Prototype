using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStats : MonoBehaviour, ITakeDamage {

    public CharacterStat MaxHealth;
    public CharacterStat MaxEnergy;
    public CharacterStat Armour;
    public CharacterStat Power;
    public CharacterStat Speed;
    public CharacterStat AttackRatio;
    public CharacterStat AttackSpeed;
    public CharacterStat AttackRange;
    public CharacterStat Luck;

    public float Health { get => health; set => health = value; }
    public float health;

    public float energy;
    public float regenRatio;

    private  float regenWait;
    private float nextRegeneration;

    void Awake () {

        MaxHealth.BaseValue = health = 100;
        MaxEnergy.BaseValue = energy = 100;
        Armour.BaseValue = 0;
        Speed.BaseValue = 2;
        Power.BaseValue = 1;
        AttackRatio.BaseValue = 10f;
        AttackSpeed.BaseValue = 1;
        AttackRange.BaseValue = 3.0f;
        Luck.BaseValue = 1;

    }

	void Update () {
        Death();
    }

    private void Regeneration()
    {

        if (Time.time > nextRegeneration)
        {
            nextRegeneration = Time.time + regenWait;
            health += regenRatio;
            if (health > MaxHealth.Value) health = MaxHealth.Value;
            energy += regenRatio;
            if (energy > MaxEnergy.Value) energy = MaxEnergy.Value;
           
        }
    }

    public void Death()
    {
        if (health <= 0) gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }  
}
