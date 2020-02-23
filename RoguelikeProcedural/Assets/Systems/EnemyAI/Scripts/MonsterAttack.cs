using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [Header("STATS")]
    public float damage;
    [SerializeField] private float projectileSpeed;
    [Header("RENDER")]
    [SerializeField] private GameObject splashEffect;
    [Header("SOUNDS")]
    [SerializeField] private AudioClip[] sparkSound;

    private Rigidbody rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();


    }
    void Start()
    {
        int soundArraySize = sparkSound.Length;
        Projectile();
        //PlayRandomSound(soundArraySize);
    }

    void Update()
    {
        Destroy(gameObject, 2.0f);
    }

    void Projectile()
    {
        rb.AddForce(transform.forward * projectileSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Splash;
            Splash = Instantiate(splashEffect);
            Splash.transform.position = this.transform.position;
            ITakeDamage damagable = other.GetComponent<ITakeDamage>();
            if(damagable != null)
            {
                damagable.TakeDamage(damage);
            }
            Destroy(this.gameObject);
            Destroy(Splash, 0.05f);
        }
    }
}
