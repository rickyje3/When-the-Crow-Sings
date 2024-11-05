using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyController component) : base(component)
    {
    }

    private void Patrol()
    {

    }







    private float enemySpeed = 0f;// = Vector3.zero;
    private Vector3 lastPosition;
    public override void FixedUpdate()
    {
        //if (Physics.Raycast(s.transform,))
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
        s.enemyMaterial.color = Color.white;
    }

    private IEnumerator setNextPoint()
    {
        var radius = 10;

        Debug.Log("Setting next point!");

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
            yield return new WaitForSeconds(s.timeToWander);
        }

        //s.navMeshAgent.destination = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));


        //yield return new WaitForSeconds(s.timeToWander);
        
        s.navMeshAgent.destination = s.transform.position;
        yield return new WaitForSeconds(s.timeToWaitBetweenWander);

        s.StartCoroutine(setNextPoint());
    }
}