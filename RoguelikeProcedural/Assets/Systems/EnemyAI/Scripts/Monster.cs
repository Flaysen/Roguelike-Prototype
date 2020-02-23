using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, ITakeDamage, IAmRigid {

    public float Health { get; set; }
    public bool IsOutOfControl { get; set; }

    public float TakeControlTime => 0.3f;

    //[Header("STATS")]
    public float health;
    public float speed;
    public float awareness;
    public float range;
    public bool isRanged = false;

   

    private Rigidbody rb;

    [SerializeField] public Material damageHighlight;

    private Material[] materials;
    private Material[] cleanColor;

    private float highlightDuration = 0.2f;
    private float higlightEnd = 0.0f;
    private bool damaged = false;

    public float attackReload = 2.0f;

    public GameObject attackPrefab;

    public static event Action<Monster> OnMonsterDeath;

    private void Awake()
    {
        Health = health;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (isRanged == false) range = 1f;

        materials = this.GetComponent<Renderer>().materials;
        cleanColor = new Material[materials.Length];
        for (int i = 0; i < cleanColor.Length; i++)
        {
            cleanColor[i] = new Material(this.GetComponent<Renderer>().materials[i]);
        }
    }
    void Update()
    {
        if (Time.time > higlightEnd && damaged == true)
        {
            RemoveDamageMaterial(damageHighlight);
        }
    }

    public void AddDamageMaterial()
    {
        higlightEnd = Time.time + highlightDuration;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.red;
        }
        damaged = true;
    }
    public void RemoveDamageMaterial(Material mat)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = cleanColor[i].color;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        AddDamageMaterial();
        if (Health <= 0)
        {
            Death();
        }
    }

    public void LoseControl()
    {
        StartCoroutine(GetControlAfterTime(TakeControlTime, () =>
        {
            rb.velocity = Vector3.zero;
        }));

    }

    public IEnumerator GetControlAfterTime(float time, Action task)
    {
        if (IsOutOfControl)
            yield break;
        IsOutOfControl = true;
        yield return new WaitForSeconds(time);
        task();
        IsOutOfControl = false;
    }

    public void Death()
    {
        OnMonsterDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
