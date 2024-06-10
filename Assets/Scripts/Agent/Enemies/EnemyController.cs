using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private LayerMask groundLayer, playerLayer;

    //Patroling
    [SerializeField]
    private Vector3 walkPoint;

    [SerializeField]
    private float walkPointRange;

    private bool walkPointSet;

    //Attacking
    [SerializeField]
    private float timeBetweenAttacks;

    private bool alreadyAttacked;

    //States
    [SerializeField]
    private float sightRange, attackRange;

    private bool playerInSightRange, playerInAttackRange;

    private bool isInitialized = false;

    public void InitializeAgent()
    {
        this.player = GameManager.Instance.playerAgent.transform;

        agent = GetComponent<NavMeshAgent>();

        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
            return;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();

        if(playerInSightRange && !playerInAttackRange)
            ChasePlayer();

        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player.position);

        if (!alreadyAttacked)
        {
            //Attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
