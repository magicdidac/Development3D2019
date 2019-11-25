using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Goomba : MonoBehaviour
{

    [SerializeField] private Transform otherPosition = null;
    [SerializeField] private float chaseDistance = 3;
    [Space]
    [SerializeField] private float patrolSpeed = 3.5f;
    [SerializeField] private float chaseSpeed = 4.5f;

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
                if (!agent.pathPending && agent.remainingDistance < .5f)
                    agent.SetDestination(NextPosition());

                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
                {
                    state = TState.CHASE;
                    agent.speed = chaseSpeed;
                    anim.SetBool("Chase", true);
                }

                break;
            case TState.CHASE:
                agent.SetDestination(player.transform.position);

                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToPlayer < 1.5f)
                {
                    state = TState.ATTACK;
                    agent.isStopped = true;
                    anim.SetBool("Chase", false);
                    anim.SetBool("Idle", true);
                }

                if (distanceToPlayer > chaseDistance)
                {
                    state = TState.PATROL;
                    agent.speed = patrolSpeed;
                    anim.SetBool("Chase", false);
                }
                break;
            case TState.ATTACK:
                Invoke("ChangeToPatrol", 1);
                break;
            case TState.DEAD:
                agent.isStopped = true;
                anim.SetTrigger("Crushed");
                break;
        }
    }

    private void ChangeToPatrol()
    {
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
