using UnityEngine;
using UnityEngine.AI;

public class MonsterTest : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private NavMeshAgent agent;

    private Monster monster;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FallowPlayer();
    }

    private void FallowPlayer()
    {
        agent.SetDestination(target.transform.position);
    }
}
