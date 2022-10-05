using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ChasingState : IState
{
    private Transform player;
    private float playerMaxDistanceRadius;
    private Agent agent;

    public Action OnPlayerRunOut;

    public ChasingState(Agent agent, Transform player, float playerMaxDistanceRadius)
    {
        this.player = player;
        this.playerMaxDistanceRadius = playerMaxDistanceRadius;
        this.agent = agent;
    }

    public void Update()
    {
        agent.transform.LookAt(agent.player.transform);
        agent.transform.Translate(Vector3.forward * Time.deltaTime * 5f);

        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, playerMaxDistanceRadius);
        if (hitColliders.Length > 0)
        {
            Collider player = hitColliders.Where( collider => collider.gameObject.tag == "Player" ).FirstOrDefault();

            if (player == null)
            {
                OnPlayerRunOut?.Invoke();
            }
        }
    }
}