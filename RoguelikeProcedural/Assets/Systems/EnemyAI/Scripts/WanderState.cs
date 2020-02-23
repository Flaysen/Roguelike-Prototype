using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{

    private MonsterController _monster;


    public WanderState(MonsterController monster) : base(monster.gameObject)
    {
        _monster = monster;
    }

    public override Type Tick()
    {
        if(_monster.CheckDistance(_monster.MonsterStats.awareness))
        {
            Debug.Log("ChaseState");
            return typeof(ChaseState);
        }

        return null;
    }

   
}
