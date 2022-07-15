/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    private void Update()
    {
        if (agent.enabled)
            agent.SetDestination(target.position);
    }
}
 */



using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class AIMovement : MonoBehaviour
{

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform destination;

    Vector3 moveVector;

    [SerializeField] Vector3 isGroundedOffset;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;

    [SerializeField] float gravityForce;
    Vector3 gravity;

    private void LateUpdate()
    {
        GetMovementInput();
        Movement();
        IsGrounded();
    }

    public void GetMovementInput()
    {
        Vector3 lookPos;
        Quaternion targetRot;

        navMeshAgent.destination = destination.position;


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

    void Movement()
    {
        // Character Controller SimpleMove applies gravity to game object
        transform.forward = Vector3.Lerp(transform.forward, moveVector, rotateSpeed * Time.deltaTime);


        if (IsGrounded())
        {
            characterController.Move(moveVector * movementSpeed * Time.deltaTime);
            /*             OnPlayerFall(false);
                        OnPlayerMovement(moveVector.magnitude > 0 ? true : false); */
        }
        else
        {
            // This gravity is assigning seperately because of test purposes
            // You can change gravity value from inspector
            // and see the result
            gravity = new Vector3(0, -gravityForce, 0);
            characterController.Move((moveVector + gravity) * movementSpeed * Time.deltaTime);
            /*             OnPlayerFall(true); */
        }

    }

    bool IsGrounded()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + isGroundedOffset, -transform.up, Color.red, 1f);

        if (Physics.Raycast(transform.position + isGroundedOffset, -transform.up, out hit, 1f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                transform.SetParent(hit.transform);
                return true;
            }
        }
        transform.SetParent(null);
        return false;

    }
}
