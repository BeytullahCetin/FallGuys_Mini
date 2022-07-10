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

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    const float gravity = -9.81f;

    private void Update()
    {
        Movement();
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
        characterController.SimpleMove(moveVector * movementSpeed);
        transform.forward = Vector3.Lerp(transform.forward, moveVector, rotateSpeed * Time.deltaTime);

        if (characterController.isGrounded && moveVector.magnitude > 0)
        {
            OnPlayerFall(false);
            OnPlayerMovement(true);
        }
        else if (moveVector.magnitude <= 0)
        {
            OnPlayerMovement(false);
        }
        else if (false == characterController.isGrounded)
        {
            OnPlayerFall(true);
        }
    }
}
