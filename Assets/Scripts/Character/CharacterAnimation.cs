using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CharacterMovement characterMovement;

    protected virtual void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();

    }

    private void OnEnable()
    {
        characterMovement.OnPlayerMovement += MoveAnimation;
        characterMovement.OnPlayerFall += FallAnimation;
        GameManager.Instance.OnWallPhaseStarted += FinishAnimation;
    }

    private void OnDisable()
    {
        characterMovement.OnPlayerMovement -= MoveAnimation;
        characterMovement.OnPlayerFall -= FallAnimation;
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
