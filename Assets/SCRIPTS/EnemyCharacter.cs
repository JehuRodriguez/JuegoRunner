using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter: BaseCharacter
{
    public float detectionRange = 10f; 
    public float attackRange = 2f; 
    public int attackDamage = 1;

    public float chargeSpeed = 10f;
    private Transform player;
    private bool isCharging = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
                isCharging = false; 
            }
            else
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * chargeSpeed * Time.deltaTime);
            }

            yield return null; 
        }
    }

    private void AttackPlayer()
    {
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(attackDamage);
            Debug.Log($"El enemigo ha atacado al jugador y le ha hecho {attackDamage} de daño.");
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
            isCharging = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

