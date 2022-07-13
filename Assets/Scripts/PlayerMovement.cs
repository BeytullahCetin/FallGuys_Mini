using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    public static Action<bool> OnPlayerMovement = delegate { };
    public static Action<bool> OnPlayerFall = delegate { };

    [SerializeField] CharacterController characterController;

    Vector2 movementInput;
    Vector3 moveVector;

    [SerializeField] Vector3 isGroundedOffset;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;

    [SerializeField] float gravityForce;
    Vector3 gravity;

    private void LateUpdate()
    {
        Movement();
        IsGrounded();
    }

    public void GetMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        moveVector.x = movementInput.x;
        moveVector.z = movementInput.y;
        moveVector = moveVector.normalized;
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
