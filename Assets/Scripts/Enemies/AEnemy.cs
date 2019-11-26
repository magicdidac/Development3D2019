using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AEnemy : MonoBehaviour
{

    [SerializeField] private Transform otherPosition = null;
    [SerializeField] private float chaseDistance = 3;
    [Space]
    [SerializeField] private float patrolSpeed = 3.5f;
    [SerializeField] private float chaseSpeed = 4.5f;
    [Space]
    [SerializeField] private GameObject deathParticles = null;

    [HideInInspector] private Animator anim;
    [HideInInspector] private Vector3 startPosition;
    [HideInInspector] private Vector3 endPosition;
    [HideInInspector] private Vector3 lastPosition;
    [HideInInspector] private TState state;
    [HideInInspector] private NavMeshAgent agent;
    [HideInInspector] private PlayerController player;

    enum TState
    {
        PATROL,
        CHASE,
        ATTACK,
        DEAD
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.instance.player;

        startPosition = transform.position;
        endPosition = otherPosition.position;

        lastPosition = endPosition;

        state = TState.PATROL;
    }

    private void Update()
    {
        switch (state)
        {
            case TState.PATROL:
                Patrol();
                break;
            case TState.CHASE:
                Chase();
                break;
            case TState.ATTACK:
                Attack();
                break;
            case TState.DEAD:
                Death();
                break;
        }
    }

    protected void Patrol()
    {
        if(player == null)
            player = GameManager.instance.player;

        if (!agent.pathPending && agent.remainingDistance < .5f)
            agent.SetDestination(NextPosition());

        if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            state = TState.CHASE;
            agent.speed = chaseSpeed;
            anim.SetBool("Chase", true);
        }
    }

    protected void Chase()
    {
        agent.SetDestination(player.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 1.5f)
        {
            state = TState.ATTACK;
            player.Hit();
            agent.isStopped = true;
            anim.SetBool("Chase", false);
            anim.SetBool("Idle", true);
            return;
        }

        if (state != TState.ATTACK && distanceToPlayer > chaseDistance)
        {
            state = TState.PATROL;
            agent.speed = patrolSpeed;
            anim.SetBool("Chase", false);
        }
    }

    protected void Attack()
    {
        Invoke("ChangeToPatrol", 1);
    }

    protected void Death()
    {
        agent.isStopped = true;
        anim.SetTrigger("Crushed");
    }

    public void DeathParticles()
    {
        Destroy(Instantiate(deathParticles, transform.position, Quaternion.identity), 1);
    }

    private void ChangeToPatrol()
    {
        CancelInvoke("ChangeToPatrol");
        state = TState.PATROL;
        anim.SetBool("Idle", false);
        agent.isStopped = false;
    }

    private Vector3 NextPosition()
    {
        if (lastPosition == endPosition)
            lastPosition = startPosition;
        else
            lastPosition = endPosition;

        return lastPosition;
    }

    public void Die()
    {
        state = TState.DEAD;

        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, .6f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, chaseDistance);

    }

}
