using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Goomba : MonoBehaviour
{

    [SerializeField] private Transform otherPosition;

    [HideInInspector] private Animator anim;
    [HideInInspector] private Vector3 startPosition;
    [HideInInspector] private Vector3 endPosition;
    [HideInInspector] private Vector3 lastPosition;
    [HideInInspector] private TState state;
    [HideInInspector] private NavMeshAgent agent;
    
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
                break;
            case TState.CHASE:

                break;
            case TState.ATTACK:

                break;
            case TState.DEAD:

                break;
        }
    }

    private Vector3 NextPosition()
    {
        if (lastPosition == endPosition)
            lastPosition = startPosition;
        else
            lastPosition = endPosition;

        return lastPosition;
    }

}
