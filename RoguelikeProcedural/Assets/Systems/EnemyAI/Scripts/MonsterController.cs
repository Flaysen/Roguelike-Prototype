using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    [SerializeField]
    public Transform Target { get; private set; }

    public Monster MonsterStats { get; private set; }

    public NavMeshAgent Agent { get; private set; }

    private MonsterStateMachine stateMachine;

    public Vector3 root = Vector3.zero;

    private void Awake()
    {       
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        MonsterStats = GetComponent<Monster>();
        stateMachine = GetComponent<MonsterStateMachine>();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states  = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(CombatState), new CombatState(this) },
             { typeof(RepositionState), new RepositionState(this) }
        };

        stateMachine.SetStates(states);
    }

  
    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public bool CheckDistance(float distance)
    {
        return (Vector3.Distance(Target.transform.position, this.transform.position) < distance) ? true : false;
    }

}
