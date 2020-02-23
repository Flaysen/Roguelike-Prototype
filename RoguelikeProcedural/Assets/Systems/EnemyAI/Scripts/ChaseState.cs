using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private MonsterController _monster;

    public ChaseState(MonsterController monster) : base(monster.gameObject)
    {
        _monster = monster;
        _monster.Agent.speed = _monster.MonsterStats.speed;
        _monster.Agent.stoppingDistance = _monster.MonsterStats.range;
    }

    public override Type Tick()
    {
        if(!_monster.CheckDistance(_monster.MonsterStats.awareness))
        {
            Debug.Log("WanderState");
            return typeof(WanderState);
        }

        if(_monster.CheckDistance(_monster.MonsterStats.range))
        {
            Debug.Log("CombatState");
            return typeof(CombatState);
        }

        FallowPlayer();

        return null;
    }

    public void FallowPlayer()
    {
        _monster.Agent.SetDestination(_monster.Target.transform.position);
    }
}
