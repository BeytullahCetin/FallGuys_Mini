using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CharacrMovement characrMovement;

    private void OnEnable()
    {
        characrMovement.OnPlayerMovement += MoveAnimation;
        characrMovement.OnPlayerFall += FallAnimation;
        GameManager.Instance.OnWallPhaseStarted += FinishAnimation;
    }

    private void OnDisable()
    {
        characrMovement.OnPlayerMovement -= MoveAnimation;
        characrMovement.OnPlayerFall -= FallAnimation;
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
