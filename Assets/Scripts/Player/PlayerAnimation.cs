using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMovement += MoveAnimation;
        PlayerMovement.OnPlayerFall += FallAnimation;
        GameManager.Instance.OnWallPhaseStarted += FinishAnimation;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMovement -= MoveAnimation;
        PlayerMovement.OnPlayerFall -= FallAnimation;
        GameManager.Instance.OnWallPhaseStarted -= FinishAnimation;
    }

    void MoveAnimation(bool state)
    {
        anim.SetBool("OnMovement", state);
    }

    void FallAnimation(bool state)
    {
        anim.SetBool("OnFalling", state);
    }

    void FinishAnimation()
    {
        anim.SetTrigger("OnVictory");
    }
}
