﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolScript : MonoBehaviour {
    NavMeshAgent agent;
    bool patrolling = false;

    [SerializeField]
    [Tooltip("This is the patrol point that it's going to start going to when it starts")]
    int targetPatrolPoint = 0;
    [SerializeField]
    private bool patrolForwards = true;
    [SerializeField]
    private float distanceToPatrolPoint = 3;
    [SerializeField]
    private bool ignoreZPosition = true;

    [Space]
    [SerializeField]
    public List<Vector3> patrolPoints; // this is the list of points it patrols to, in a loop probably unless said otherwise


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (patrolling)
        {
            if (ignoreZPosition)
            {
                // use vector2.distance so that it ignores z position
                if (Vector2.Distance(transform.position, patrolPoints[targetPatrolPoint]) < distanceToPatrolPoint)
                {
                    NextPatrolPoint();
                }
            } else
            {
                if (Vector3.Distance(transform.position, patrolPoints[targetPatrolPoint]) < distanceToPatrolPoint)
                {
                    NextPatrolPoint();
                }
            }
        }
    }

    public void NextPatrolPoint()
    {
        if (patrolForwards)
        {
            targetPatrolPoint++;
            targetPatrolPoint %= patrolPoints.Count;
        } else
        {
            targetPatrolPoint--;
            if (targetPatrolPoint < 0)
            {
                targetPatrolPoint = patrolPoints.Count - 1;
            }
        }
        agent.SetDestination(patrolPoints[targetPatrolPoint]);
    }

    [ContextMenu("Start Patrolling")]
    public void StartPatrolling()
    {
        patrolling = true;
        targetPatrolPoint = GetClosestPathPoint();
        agent.SetDestination(patrolPoints[targetPatrolPoint]);
    }

    [ContextMenu("Stop Patrolling")]
    public void StopPatrolling()
    {
        patrolling = false;
    }

    private int GetClosestPathPoint()
    {
        // gets the index of the closest path point.
        int closest = 0;
        NavMeshPath currentPath = agent.path;
        agent.SetDestination(patrolPoints[0]);
        float minDistance = agent.remainingDistance;
        for (int i = 1; i < patrolPoints.Count; i++)
        {
            agent.SetDestination(patrolPoints[i]);
            if (agent.remainingDistance < minDistance)
            {
                minDistance = agent.remainingDistance;
                closest = i;
            }
        }
        agent.path = currentPath;
        return closest;
    }
}
