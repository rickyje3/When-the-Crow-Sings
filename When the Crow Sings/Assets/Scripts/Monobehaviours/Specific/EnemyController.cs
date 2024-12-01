using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : StateMachineComponent
{
    public List<EnemyWaypointsHolder> enemyWaypointsHolders;
    public float patrolSpeed = 4.5f;
    public float pursuitSpeed = 10.0f;

    [HideInInspector]
    public EnemyWaypoint currentWaypoint;

    public Animator enemyAnimator;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    public float timeToWanderIfNoWaypoint = 4.0f;
    public float timeToWaitBetweenWander = 2.0f;
    public float timeToBeStunned = 2.0f;
    public float lookAtHeight = 2.5f;

    bool lastTime = false;
    RaycastHit hit;

    [HideInInspector]
    public bool canSeePlayer = false;

    public EnemySightCone enemySightCone;
    public Transform raycastStart;
    public List<LineRenderer>  lineRenderers;

    [HideInInspector] public EnemyWaypointsHolder currentWaypointHolder;
    public bool doesSeePlayer
    {
        get
        {
            return canSeePlayer && enemySightCone.playerInSightCone;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new EnemyPatrolState(this), "EnemyPatrolState");
        stateMachine.RegisterState(new EnemyChaseState(this), "EnemyChaseState");
        stateMachine.RegisterState(new EnemyStunnedState(this), "EnemyStunnedState");
        stateMachine.RegisterState(new EnemyIdleState(this), "EnemyIdleState");
        stateMachine.Enter("EnemyIdleState");
    }
    private void Start()
    {
        if (enemyWaypointsHolders == null)
        {
            throw new System.Exception("No enemy waypoint holder assigned!");
        }
        else
        {
            currentWaypointHolder = enemyWaypointsHolders[0];
            currentWaypoint = currentWaypointHolder.waypoints[0];
        }
        
    }

    public void OnSpotPlayerRegardlessTriggerEntered(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            stateMachine.Enter("EnemyChaseState");
    }

    public void EnterChaseStateSafe()
    {
        Debug.Log("EnterChaseStateSafe() called!");
        if (!isWaitingToCheckCanSeePlayer) StartCoroutine(checkIfStillDoesSeePlayer());
    }
    bool isWaitingToCheckCanSeePlayer = false;
    public float bufferBeforeSeesPlayer = .2f;
    IEnumerator checkIfStillDoesSeePlayer()
    {
        isWaitingToCheckCanSeePlayer = true;
        yield return new WaitForSeconds(bufferBeforeSeesPlayer);
        if (canSeePlayer) stateMachine.Enter("EnemyChaseState");
        isWaitingToCheckCanSeePlayer = false;
    }

    //public void SightConeTriggerEntered(Collider other)
    //{
    //    //stateMachine.OnTriggerEnter(other);
    //}

    private void OnTriggerEnter(Collider other)
    {
        stateMachine.OnTriggerEnter(other); // This is in the StateMachineComponent and shouldn't be duplicated ideally...

        if (other.GetComponent<BirdBrain>())
        {
            stateMachine.Enter("EnemyStunnedState");
        }
    }
    public void SightConeTriggerExited(Collider other)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }


    
    //public void SightConeTriggerStay(Collider other)
    //{
        
    //}

    private void FixedUpdate()
    {
        if ( ServiceLocator.Get<PlayerController>() != null )
        {
            Vector3 targetPosition = ServiceLocator.Get<PlayerController>().transform.position;
            targetPosition.y += lookAtHeight;

            List<Vector3> endPoints = new List<Vector3>();
            endPoints.Add(targetPosition);
            endPoints.Add(new Vector3(targetPosition.x, targetPosition.y -= 3.0f,targetPosition.z));
            if (lastTime)
            {
                targetPosition.y -= 3.0f;
            }
            lastTime = !lastTime;

            //RenderRayCastLine(targetPosition);
            //Vector3 endPoint = targetPosition;
            


            if (Physics.Raycast(raycastStart.position, targetPosition - transform.position, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                    //endPoint = hit.transform.position;
                }
                
            }
            RenderRayCastLine(endPoints);
        }
        stateMachine.FixedUpdate();
    }

    private void RenderRayCastLine(List<Vector3> targetPositions)
    {
        if (DebugManager.showCollidersAndTriggers)
        {
            foreach (Vector3 pos in targetPositions)
            {
                LineRenderer lineRenderer = lineRenderers[targetPositions.IndexOf(pos)];
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, pos);
                if (canSeePlayer) lineRenderer.startColor = Color.red;
                else lineRenderer.startColor = Color.green;
            }
        }
        else
        {
            foreach (LineRenderer i in lineRenderers)
            {
                i.enabled = false;
            }
        }
        
        
    }

    public void OnChangeWaypointsTriggered(SignalArguments args)
    {
        if (args.objectArgs[0] == this)
        {
            if (enemyWaypointsHolders.Contains((EnemyWaypointsHolder)args.objectArgs[1]))
            {
                currentWaypointHolder = (EnemyWaypointsHolder)args.objectArgs[1];
            }
        }
    }

    //private void FixedUpdate()
    //{
    //    RaycastHit hit;

    //    Vector3 targetPosition = ServiceLocator.Get<PlayerController>().transform.position;
    //    targetPosition.y += lookAtHeight;


    //    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    //    lineRenderer.enabled = true;
    //    lineRenderer.SetPosition(0, transform.position);
    //    lineRenderer.SetPosition(1, targetPosition);



    //    if (Physics.Raycast(transform.position, targetPosition - transform.position, out hit))
    //    {
    //        if (hit.transform.tag == "Player")
    //        {
    //            Debug.Log("I SEE YOU");
    //        }
    //    }
    //}
}
