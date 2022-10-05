using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private StateMachine stateMachine;
    private List<IState> states;

    public Transform player;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float playerDetectorRadius;
    [SerializeField] private float playerMaxDistanceRadius;

    PatrollingState patrollingState;
    ChasingState chasingState;

    void Start()
    {
        states = new List<IState>();

        patrollingState = new PatrollingState(new Queue<Transform>(waypoints), transform, playerDetectorRadius);
        chasingState = new ChasingState(this as Agent, player, playerMaxDistanceRadius);

        states.Add(patrollingState);
        states.Add(chasingState);

        patrollingState.OnPlayerEnterInRange += OnPlayerInRangeHandler;
        chasingState.OnPlayerRunOut += OnPlayerRunOutHandler;

        stateMachine = new StateMachine(states);
    }

    private void OnPlayerRunOutHandler()
    {
        stateMachine.SetState(patrollingState);
    }

    private void OnPlayerInRangeHandler(GameObject player)
    {
        this.player = player.transform;
        stateMachine.SetState(chasingState);
    }

    void Update()
    {
        if (stateMachine != null)
            stateMachine.Update();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectorRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerMaxDistanceRadius);
    }
}
