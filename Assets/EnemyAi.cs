using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;

    public float timeBetweenAttack = 1f;
    public bool alreadyAttacked = false;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator anim;
    private AudioSource audioSource;
    public AudioClip thud;
    public GameObject terrain;

    private void Awake()
    {
        player = GameObject.Find("MC@Breathing Idle").transform;
        agent = GetComponent<NavMeshAgent>();
        audioSource=GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        anim.SetBool("isRunning", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
{
    anim.SetBool("isRunning", false);
    agent.SetDestination(transform.position);
    transform.LookAt(player);
    anim.SetBool("isAttacking",true);

}

    private void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("isAttacking",false);
    }

    private void StopChasingPlayer()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking",false);
        agent.SetDestination(transform.position);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopChasingPlayer();
        }
    }
}