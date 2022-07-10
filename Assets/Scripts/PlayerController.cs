using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform startTransform;

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        var hit = other.gameObject.GetComponent<Obstacle>();
        if (hit)
        {
            foreach (Collider col in hit.GetColliders)
            {
                if (col.name == other.collider.name)
                {
                    Die();
                    return;
                }
            }
        }
    }

    public void Die()
    {
        characterController.enabled = false;
        transform.position = startTransform.position;
        characterController.enabled = true;
    }
}
