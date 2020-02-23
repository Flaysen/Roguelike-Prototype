using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnergySpark : MonoBehaviour {

    public float damage;

    [SerializeField] private float projectileSpeed;

    [SerializeField] private GameObject effect;

    [SerializeField] private GameObject splashEffect;

    public PlayerStats stats;

    private Rigidbody rb;

    // Use this for initialization
    void Awake() {
        rb = GetComponent<Rigidbody>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void Start() => FireProjectile();


    void Update() => Destroy(gameObject,
        CalculateProjectileDestroy(stats.AttackRange.Value, projectileSpeed));

    void FireProjectile()
    {
        transform.localScale = CalculateProjectileScale(stats.Power.Value);
        projectileSpeed = CalculateProjectileSpeed(stats.AttackSpeed.Value);
        rb.AddForce(transform.forward * projectileSpeed);
    }

    private float CalculateProjectileSpeed(float projectileSpeed)
    {
        return projectileSpeed * 50f + 200.0f;
    }

    private float CalculateProjectileDestroy(float projectileRange, float projectileSpeed)
    {
        return projectileRange / (projectileSpeed / 200.0f);
    }
       
    private Vector3 CalculateProjectileScale(float power)
    {
        float scale = 0.2f + 0.025f * power;
        return new Vector3(scale, scale, scale);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Projectile") == false && other.collider.CompareTag("Player") == false)
        {
            GameObject Splash;
            Splash = Instantiate(splashEffect);
            Splash.transform.position = this.transform.position;
            Destroy(this.gameObject);
            Destroy(Splash, 0.05f);



            ITakeDamage damagable = other.collider.GetComponent<ITakeDamage>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
                GameObject Effect;
                //Effect = Instantiate(effect, other.transform.position, Quaternion.identity,
                  //  other.transform);

                // Effect.transform.position = other.transform.position;
                //Effect.transform.parent = other.transform;
            }

        }
    }
}
