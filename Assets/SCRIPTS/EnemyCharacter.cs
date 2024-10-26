using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter: BaseCharacter
{
    public float detectionRange = 10f; 
    public float attackRange = 2f; 
    public float attackDamage = 1; 

    private Transform player;
    private float attackCooldown = 1f; 
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        base.Update();
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            MoveTowardsPlayer();

            if (distanceToPlayer < attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time; 
            }
        }
    }

    private void MoveTowardsPlayer()
    {
       
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Attack()
    {
        
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage((int)attackDamage); 
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

