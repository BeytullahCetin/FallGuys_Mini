using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMovement += MoveAnimation;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMovement -= MoveAnimation;
    }

    void MoveAnimation(bool state)
    {
        anim.SetBool("OnMovement", state);
    }
}
