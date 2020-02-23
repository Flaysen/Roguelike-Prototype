using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAmRigid  
{
    bool IsOutOfControl { get; set; }

    float TakeControlTime { get; }
    void LoseControl();

    IEnumerator GetControlAfterTime(float time, Action task);
}
