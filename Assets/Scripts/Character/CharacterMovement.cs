using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacrMovement : MonoBehaviour
{
    public Action<bool> OnPlayerMovement = delegate { };
    public Action<bool> OnPlayerFall = delegate { };

    [SerializeField] CharacterController characterController;

    [SerializeField] protected Vector3 isGroundedOffset;

    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected float rotateSpeed = 1f;

    [SerializeField] protected float gravityForce;
    
    protected Vector3 gravity;
    protected Vector3 moveVector;

    protected virtual void LateUpdate()
    {
        Movement();
        IsGrounded();
    }

    void Movement()
    {
        // Character Controller SimpleMove applies gravity to game object
        transform.forward = Vector3.Lerp(transform.forward, moveVector, rotateSpeed * Time.deltaTime);


        if (IsGrounded())
        {
            characterController.Move(moveVector * movementSpeed * Time.deltaTime);
            OnPlayerFall(false);
            OnPlayerMovement(moveVector.magnitude > 0 ? true : false);
        }
        else
        {
            // This gravity is assigning seperately because of test purposes
            // You can change gravity value from inspector
            // and see the result
            gravity = new Vector3(0, -gravityForce, 0);
            characterController.Move((moveVector + gravity) * movementSpeed * Time.deltaTime);
            OnPlayerFall(true);
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
