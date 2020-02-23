using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    private float maxHeight = 1f;
    private void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("Player") && transform.position.y < maxHeight)
        {
            Debug.Log("trigger");
            transform.position += transform.up * speed * Time.deltaTime;
        }
            
    }
 
}
