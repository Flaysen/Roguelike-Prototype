using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplode
{  
    GameObject ExplosionPrefab { get; }
    float ExplosionRadius { get; }
    void Explode();
}


