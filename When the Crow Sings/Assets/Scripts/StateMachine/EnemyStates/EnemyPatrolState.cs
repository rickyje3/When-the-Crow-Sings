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








    public override void FixedUpdate()
    {
        //if (Physics.Raycast(s.transform,))
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("I SEE YOU THERE IS NO LIFE IN THE VOID DIE NOW");
        s.stateMachine.Enter("EnemyChaseState");
    }


    public override void StateEntered()
    {
        s.StartCoroutine(setRandomPoint());
    }

    private IEnumerator setRandomPoint()
    {
        var radius = 10;
        s.navMeshAgent.destination = new Vector3(Random.Range(-10,10), Random.Range(-10, 10), Random.Range(-10, 10));
        yield return new WaitForSeconds(s.timeToWander);
        s.navMeshAgent.destination = s.transform.position;
        yield return new WaitForSeconds(s.timeToWaitBetweenWander);

        s.StartCoroutine(setRandomPoint());
    }
}