using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingsObject : MonoBehaviour
{
    [SerializeField]
    private GameObject [] _prefabs;

    //private Transform _transform;

    //private Renderer _renderer;

    [SerializeField]
    private float _scaleDeviation;

    [SerializeField]
    private float _rotationDeviation;

    [SerializeField]
    private Material[] materials;

    private void Start()
    {
        Invoke("SpawnObject", 1.0f);
    }
    private Vector3 SetPrefabPosition()
    {
        return new Vector3(transform.position.x,
            transform.position.y + (transform.localScale.y - 0.1f) / 2,
            transform.position.z);
    }

    private Vector3 SetPrefabScale()
    {
        return new Vector3(
            Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation),
            Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation),
            Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation));
    }

    private Quaternion SetPrefabRotation()
    {
        return Quaternion.Euler(
            Random.Range(-_rotationDeviation, _rotationDeviation),
            Random.Range(0, 360),
            Random.Range(-_rotationDeviation, _rotationDeviation));
    }

    private Material SetMaterial()
    {
        return materials[Random.Range(0, materials.Length)];
    }

    private void SpawnObject()
    {
        GameObject @object = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform.position, SetPrefabRotation());
        @object.transform.localScale = SetPrefabScale();
        @object.GetComponentInChildren<Renderer>().material = SetMaterial();
    }
}
