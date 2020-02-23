using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IExplode, ITakeDamage
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField] private float explosionForce = 300.0f;

    public float ExplosionRadius { get; } = 3;

    public float Health { get; set; }

    public GameObject ExplosionPrefab => explosionPrefab;

    [SerializeField] List<GameObject> barrelParts = new List<GameObject>();

    private void Awake()
    {
        Health = 1;
    }
    public void Explode()
    {
        Instantiate(explosionPrefab,
            new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z),
            Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach(Collider c in hitColliders)
        {
            IAmRigid pushable = c.GetComponent<IAmRigid>();
            if(pushable != null)
            {
                pushable.LoseControl();
                Rigidbody rb = c.GetComponent<Rigidbody>();
                rb?.AddExplosionForce(explosionForce, transform.position, ExplosionRadius);
            }
        }

        foreach(GameObject @object in barrelParts)
        {
            @object.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Death();
    }

    public void Death()
    {
        Invoke("Explode", 0);
        Destroy(gameObject, 0.8f);

    }
}
