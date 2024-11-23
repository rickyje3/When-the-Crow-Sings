using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyController component) : base(component) {}

    private float enemySpeed = 0f;
    private Vector3 lastPosition;
    public override void FixedUpdate()
    {
        if (lastPosition == null)
        {
            lastPosition = s.transform.position;
        }

        enemySpeed = (s.transform.position - lastPosition).magnitude;
        lastPosition = s.transform.position;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("I SEE YOU THERE IS NO LIFE IN THE VOID DIE NOW");
        s.stateMachine.Enter("EnemyChaseState");
    }


    public override void StateEntered()
    {
        s.StartCoroutine(setNextPoint());
    }
    public override void StateExited()
    {
        s.enemyAnimator.SetBool("animIsPatrolWalking", false);
    }

    private IEnumerator setNextPoint()
    {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        var radius = 10;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

        Debug.Log("Setting next point!");
        s.enemyAnimator.SetBool("animIsPatrolWalking", true);

        if (s.currentWaypoint != null)
        {
            s.currentWaypoint = s.enemyWaypointsHolders[0].GetNextWaypoint(s.currentWaypoint);
            s.navMeshAgent.destination = s.currentWaypoint.transform.position;
            yield return new WaitUntil(() => enemySpeed <= 0.01f);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => enemySpeed <= 0.01f);
        }
        else
        {
            s.navMeshAgent.destination = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(s.timeToWanderIfNoWaypoint);
        }
        
        s.navMeshAgent.destination = s.transform.position;
        s.enemyAnimator.SetBool("animIsPatrolWalking", false);
        yield return new WaitForSeconds(s.timeToWaitBetweenWander);
        s.StartCoroutine(setNextPoint());
    }
}