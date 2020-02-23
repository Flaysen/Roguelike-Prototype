using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Spell, IExplode
{
    private Vector3 target;

    private Collider collider;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float fallSpeed = 2.0f;

    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private GameObject explosionPrefab;

    public GameObject ExplosionPrefab => explosionPrefab;

    public float ExplosionRadius => 3.0f;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void Start()
    {
        target = transform.position;
        transform.position += startPosition;
        this.transform.LookAt(target);
    }
    private void Update()
    {
        Fall();
        Destroy(gameObject, 2.0f);
    }

    void Fall()
    {
        transform.Translate(0, 0, fallSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        ITakeDamage damagable = other.GetComponent<ITakeDamage>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void Explode()
    {
        Instantiate(explosionPrefab,
            new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z),
            Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider c in hitColliders)
        {
            IAmRigid pushable = c.GetComponent<IAmRigid>();
            if (pushable != null)
            {
                pushable.LoseControl();
                Rigidbody rb = c.GetComponent<Rigidbody>();
                rb?.AddExplosionForce(300.0f, transform.position, ExplosionRadius);
            }
        }
    }
    private void OnDestroy()
    {
        Explode();
    }
}
