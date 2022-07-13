using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wall : MonoBehaviour
{
    private void Update()
    {

    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Fire Input: performed");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
