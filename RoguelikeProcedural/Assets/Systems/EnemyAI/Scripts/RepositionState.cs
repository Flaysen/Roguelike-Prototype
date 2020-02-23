using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RepositionState : BaseState
{
    private MonsterController _monster;

    private Vector3 _desiredPosition = Vector3.zero;

    private float nextAttackTime = 0.0f;

    private float repositionTime = 2.0f;

    public RepositionState(MonsterController monster) : base(monster.gameObject)
    {
        _monster = monster;
    }
    public override Type Tick()
    {
        if(_desiredPosition == Vector3.zero)
        {
            _desiredPosition = FindPositions(_monster.root);
        }

        Reposition(_desiredPosition);

        if (Time.time > nextAttackTime)
        {
            _desiredPosition = Vector3.zero;
            return typeof(ChaseState);
        }

        return null;
    }

    private Vector3 FindPositions(Vector3 root)
    {
        nextAttackTime = repositionTime + Time.time;
        return new Vector3(UnityEngine.Random.Range(root.x + 4, root.x - 4),
               _monster.transform.position.y, UnityEngine.Random.Range(root.z + 4, root.z - 4));
    }

    private void Reposition(Vector3 target)
    {
        _monster.Agent.SetDestination(target);
    }
}
