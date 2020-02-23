using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapFix : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            if(transform.position.x != other.transform.position.x)
                Destroy(transform.position.x > other.transform.position.x ?
                    gameObject : other.gameObject);
            else
                Destroy(transform.position.z > other.transform.position.z ?
                    gameObject : other.gameObject);

        }
    }
}
