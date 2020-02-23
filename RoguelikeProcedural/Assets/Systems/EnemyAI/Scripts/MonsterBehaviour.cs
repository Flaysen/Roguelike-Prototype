using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class MonsterBehaviour : MonoBehaviour
{
    private GameObject target;
    private Monster monster;



    int side;

    public GameObject attackPrefab;

    [SerializeField] private float attackReload = 2.0f;
    private float nextAttackTime = 0.0f;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        monster = GetComponent<Monster>();
    }
    private void Update()
    {
        if(CheckDistance(monster.awareness))
        {
            TurnToFacePlayer();

            if (CheckDistance(monster.range))
            {
                if (CanAttack())
                {
                    Attack();
                    side = ChangeSide();
                }
                else SideStep(side);
            }
            else FallowPlayer();
        }      
    }

    private bool CheckDistance(float distance)
    {
        return (Vector3.Distance(target.transform.position, this.transform.position) < distance) ? true : false;
    }
    
    private void FallowPlayer()
    {       
       
        this.transform.Translate(0, 0, monster.speed * Time.deltaTime);           
    }

    private void TurnToFacePlayer()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                    Quaternion.LookRotation(direction), 0.1f);
    }
   
    private void SideStep(int side)
    {
        transform.localPosition += transform.right * side * monster.speed * Time.deltaTime;
    }

    private int ChangeSide()
    {
        System.Random rand = new System.Random();
        return rand.Next(0, 2) * 2 - 1;        
    }
    
    private bool CanAttack()
    {
        return (Time.time > nextAttackTime) ? true : false;
    }
    public void Attack()
    {
        Instantiate(attackPrefab, transform.position + transform.forward.normalized
               + new Vector3(0, transform.lossyScale.y, 0), transform.rotation);

        nextAttackTime = Time.time + attackReload;
    }
}
