using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{ 
    public GameObject objectPrefab;
    public int poolSize;
    public Queue<Transform> objectQueue = new Queue<Transform>();
    //public Transform plane;
    private GameObject[] objectArray;
    public static Pool poolSingleton;

    private void Awake()
    {
        if(poolSingleton != null)
        {
            Destroy(GetComponent<Pool>());
            return;
        }

        poolSingleton = this;
        objectArray = new GameObject[poolSize];

        for(int i=0; i<poolSize; i++)
        {
            objectArray[i] = Instantiate(objectPrefab, Vector3.zero,
                Quaternion.identity) as GameObject;
            Transform objectTransform = objectArray[i].GetComponent<Transform>();
            objectQueue.Enqueue(objectTransform);
            objectArray[i].SetActive(false);
        } 
    }
    
    public static Transform SpawnObject(Vector3 position, Quaternion rotation)
    {
        Transform spawnedObject = poolSingleton.objectQueue.Dequeue();
        spawnedObject.gameObject.SetActive(true);
        spawnedObject.position = position;
        spawnedObject.localRotation = rotation;

        poolSingleton.objectQueue.Enqueue(spawnedObject);

        return spawnedObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
