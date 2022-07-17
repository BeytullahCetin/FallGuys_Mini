using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class AIMovement : PlayerMovement
{

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform destination;

    Vector3 randomOffset;

    private void Start()
    {
        randomOffset = new Vector3(UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 5));
    }

    protected override void LateUpdate()
    {
        GetMovementInput();
        base.LateUpdate();
    }

    public void GetMovementInput()
    {
        Vector3 lookPos;
        Quaternion targetRot;

        navMeshAgent.destination = destination.position + randomOffset;


        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        lookPos = this.destination.position - this.transform.position;
        lookPos.y = 0;
        targetRot = Quaternion.LookRotation(lookPos);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);


        moveVector = navMeshAgent.desiredVelocity;
        moveVector.y = 0;
        moveVector = moveVector.normalized;

        navMeshAgent.nextPosition = transform.position;
    }
}
