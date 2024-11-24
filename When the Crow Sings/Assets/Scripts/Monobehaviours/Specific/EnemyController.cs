using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : StateMachineComponent
{
    public List<EnemyWaypointsHolder> enemyWaypointsHolders;
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
            currentWaypoint = enemyWaypointsHolders[0].waypoints[0];
        }
        
    }

    //public void SightConeTriggerEntered(Collider other)
    //{
    //    //stateMachine.OnTriggerEnter(other);
    //}
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
            RenderRayCastLine(targetPosition);

            if (lastTime)
            {
                targetPosition.y -= 3.0f;
            }
            lastTime = !lastTime;

            if (Physics.Raycast(transform.position, targetPosition - transform.position, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    //stateMachine.OnTriggerEnter(hit.transform.GetComponent<Collider>());
                    canSeePlayer = true;
                    Debug.Log("SEEEE YOU");
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }
        stateMachine.FixedUpdate();
    }

    private void RenderRayCastLine(Vector3 targetPosition)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPosition);
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
