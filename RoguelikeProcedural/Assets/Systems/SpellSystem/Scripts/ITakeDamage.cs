using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    float Health { get; set; }
    void TakeDamage(float damage);

    void Death();
}
