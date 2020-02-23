using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{

    public Rigidbody projectile;

    [SerializeField] private float baseCooldown;

    private PlayerController playerController;

    private PlayerStats stats;

    private float nextFireTime;

    public float fireCooldown;

    private bool canFire;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        stats = GameObject.Find("Player").GetComponent<PlayerStats>();

        playerController.BasicAttack += HandleFire;
    }

    void Update()
    {
        //fireCooldown = stats.AttackRatio.Value ;

        fireCooldown = CalculateCooldown(stats.AttackRatio.Value);

        if (Time.time > nextFireTime)
            canFire = true;
    }

    private void HandleFire()
    {
        if(canFire)
        {
            Instantiate(projectile, transform.position, transform.rotation);

            canFire = false;
            nextFireTime = Time.time + fireCooldown;
        }

    } 

    private float CalculateCooldown(float attackRatio)
    {
        return 1 / (attackRatio - 1);
    }
}
