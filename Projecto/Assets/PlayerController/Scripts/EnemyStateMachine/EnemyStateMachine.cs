using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector] private AState _currentState;

    [HideInInspector]
    public AState currentState
    {
        get { return _currentState; }

        set
        {
            if (value == null)
                return;

            if (_currentState != null)
                _currentState.DoExit();

            _currentState = value;

        }
    }

    [Space]
    [Header("Enemy AI Parameters")]
    [SerializeField] public bool recievedDamage = false;
    [SerializeField] public bool recievedCritical = false;

    [SerializeField] public bool listenPlayer = false;
    [SerializeField] public bool seePlayer = false;
    [SerializeField] public bool directCastPlayer = false;

    [SerializeField] public float minChaseDistance = 2;
    [SerializeField] public float maxChaseDistance = 8;
    [SerializeField] public float maxAttackDistance = 5;
    [SerializeField] private int damage = 30; 

    [SerializeField] public GameObject bullet = null;
    [SerializeField] public Dissolve dissolveModel = null;
    [Space]
    [SerializeField] public List<Transform> positions = new List<Transform>();


    [HideInInspector] public Vector3 initialPos;
    [HideInInspector] public int posIndex;

    [HideInInspector] private FieldOfView fow;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public EnemyDamager damager;


    [HideInInspector] public Transform player;

    private void Start()
    {
        fow = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        damager = GetComponent<EnemyDamager>();
        damager.self = this;

        player = GameManager.instance.player.transform;

        initialPos = transform.position;
        posIndex = -1;

        currentState = new Patrol_ES(this);
    }

    private void Update()
    {
        currentState.DoUpdate();

        directCastPlayer = !Physics.Raycast(transform.position, (player.position - transform.position).normalized, (player.position - transform.position).magnitude, fow.obstacleMask);

    }

    private void FixedUpdate()
    {
        seePlayer = fow.SeePlayer();
        listenPlayer = fow.ListenPlayer();

        currentState = currentState.ChangeState();
        currentState.DoFixedUpdate();


        recievedDamage = false;
        recievedCritical = false;
    }

    private bool IsCurrentType(Type type)
    {
        return currentState.GetType() == type;
    }

    public void SetNearestIndex()
    {
        int nearestIndex = 0;

        for(int i = 1; i < positions.Count; i++)
        {
            if(PointDistance(positions[nearestIndex].position) > PointDistance(positions[i].position))
            {
                nearestIndex = i;
            }
        }

        posIndex = nearestIndex;

    }

    public Vector3 GetNextPosition()
    {
        posIndex++;
        if (posIndex < positions.Count)
            return positions[posIndex].position;

        posIndex = -1;
        return initialPos;

    }

    private float PointDistance(Vector3 point)
    {
        return (point - transform.position).magnitude;
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            GameObject.Instantiate(bullet, transform.GetChild(0).position, Quaternion.identity).GetComponent<EnemyBullet>().damage = this.damage;
            GameManager.instance.audioManager.PlaySound("DroneShoot");
            AttackRoutine();
        }
    }

}
