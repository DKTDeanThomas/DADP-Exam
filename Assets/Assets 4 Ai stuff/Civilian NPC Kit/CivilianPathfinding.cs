using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CivilianPathfinding : MonoBehaviour
{
    public bool isStationary = false;
    public float moveSpeed = 1f;
    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    [Header("Patrol Delay")]
    public List<float> patrolDelays = new List<float>();
    private float currentPatrolDelay;
    private bool isWaitingAtPatrolPoint = false;

    public float minSpeed = 0.5f;
    public float maxSpeed = 1.5f;

    public float minDelay = 1f;
    public float maxDelay = 5f;

    private bool isMovementLocked = false;

    public void LockMovement(bool shouldLock)
    {
        isMovementLocked = shouldLock;
        if (shouldLock)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomSpeed();
        if (patrolPoints.Count > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        if (isStationary || isMovementLocked) return;
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaitingAtPatrolPoint)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }

    void SwitchTarget()
    {
    currentPatrolIndex = Random.Range(0, patrolPoints.Count);
    agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private IEnumerator WaitAtPatrolPoint()
    {
    isWaitingAtPatrolPoint = true;
    currentPatrolDelay = Random.Range(minDelay, maxDelay);
    yield return new WaitForSeconds(currentPatrolDelay);
    isWaitingAtPatrolPoint = false;
    SwitchTarget();
    }

    void SetRandomSpeed()
    {
    agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    public void ResetPatrol()
    {
        if (patrolPoints.Count == 0) return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
}
