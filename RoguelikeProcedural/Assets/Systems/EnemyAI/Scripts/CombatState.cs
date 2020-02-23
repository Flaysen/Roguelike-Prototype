using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatState : BaseState
{
    private MonsterController _monster;

    private float nextAttackTime = 0.0f;

    private int side;

    private NavMeshSurface surface;

    public CombatState(MonsterController monster) : base(monster.gameObject)
    {
        _monster = monster;
    }

    public override Type Tick()
    {
        if(!_monster.CheckDistance(_monster.MonsterStats.range))
        {
            return typeof(ChaseState);
        }

        TurnToFacePlayer();

        if (CanAttack())
        {
            Attack();

            //side = ChangeSide();
        }
        else if(_monster.MonsterStats.isRanged == true) SideStep(side);

        return null;
    }

    public void Attack()
    {
        UnityEngine.Object.Instantiate(_monster.MonsterStats.attackPrefab, transform.position + transform.forward.normalized
               + new Vector3(0, transform.lossyScale.y, 0), transform.rotation);

        nextAttackTime = Time.time + _monster.MonsterStats.attackReload;
    }

    private bool CanAttack()
    {
        return (Time.time > nextAttackTime) ? true : false;
    }

    private void TurnToFacePlayer()
    {
        Vector3 direction = _monster.Target.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                    Quaternion.LookRotation(direction), 0.1f);
    }

    private void SideStep(int side)
    {
        transform.localPosition += transform.right
            * side * _monster.MonsterStats.speed * 0.5f * Time.deltaTime;
    }

    private int ChangeSide()
    {
        System.Random rand = new System.Random();
        return rand.Next(0, 2) * 2 - 1;
    }

}
