using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 1;

    public float chargeSpeed = 10f;
    public float damageCooldown = 1.0f;
    private Transform player;
    private bool isCharging = false;
    private Vector3 initialPosition;
    private float lastAttackTime = 0f;

    private PlayerCharacter playerCharacter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCharacter = player.GetComponent<PlayerCharacter>();
        initialPosition = transform.position;
    }

    protected override void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && !isCharging)
        {
            StartCoroutine(ChargeAtPlayer());
        }
       
    }

    private IEnumerator ChargeAtPlayer()
    {
        isCharging = true;

        while (isCharging)
        {
            
            float distanceToPlayer = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(player.position.x, 0, player.position.z)
            );

           
            float heightDifference = Mathf.Abs(player.position.y - transform.position.y);

           
            if (distanceToPlayer < attackRange && heightDifference < 1f && playerCharacter != null && playerCharacter.IsGrounded() && Time.time >= lastAttackTime + damageCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
           
            else if (distanceToPlayer > attackRange * 0.75f && distanceToPlayer < detectionRange && heightDifference < 1f)
            {
                Vector3 direction = (new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position).normalized;
                transform.Translate(direction * chargeSpeed * Time.deltaTime, Space.World);
            }

            else if (distanceToPlayer <= attackRange * 0.75f)
            {
                isCharging = false;
            }

            else 
            {
                Vector3 directionToInitial = (initialPosition - transform.position).normalized;
                transform.Translate(directionToInitial * chargeSpeed * Time.deltaTime, Space.World);

                
                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    isCharging = false;
                }
            }


            yield return null;
        }

    
    }


    private void AttackPlayer()
    {
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(attackDamage);
           
        }
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

