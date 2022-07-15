using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : CharacrMovement
{

    Vector2 movementInput;

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void GetMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        moveVector.x = movementInput.x;
        moveVector.z = movementInput.y;
        moveVector = moveVector.normalized;
    }
}
