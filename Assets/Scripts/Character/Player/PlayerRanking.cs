using UnityEngine;
using TMPro;

public class PlayerRanking : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerRankingText;

    [SerializeField] Transform player;
    [SerializeField] Transform[] AIs;
    [SerializeField] TextMeshProUGUI congratsText;

    int currentRanking;
    bool isRankingFinalized = false;

    private void OnEnable()
    {
        GameManager.Instance.OnWallPhaseStarted += FinalizeRanking;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnWallPhaseStarted -= FinalizeRanking;
    }

    private void FinalizeRanking()
    {
        isRankingFinalized = true;

        SetCongratsText();
    }

    private void SetCongratsText()
    {
        if (currentRanking <= 3)
        {
            congratsText.SetText("Congratulations!\n\n" +
            $"You finished {playerRankingText.text}");
        }
        else
        {
            congratsText.SetText("You lost!\n" +
            "Wanna try again?");
        }
    }

    private void Update()
    {
        CheckCurrentRanking();
    }

    private void CheckCurrentRanking()
    {
        if (isRankingFinalized)
            return;

        currentRanking = AIs.Length + 1;

        foreach (Transform aiTransform in AIs)
        {
            if (player.position.z > aiTransform.position.z)
            {
                currentRanking--;
            }
        }

        playerRankingText.SetText(currentRanking.ToString());

        switch (currentRanking)
        {
            case 1:
                playerRankingText.text += "st";
                break;
            case 2:
                playerRankingText.text += "nd";
                break;
            case 3:
                playerRankingText.text += "rd";
                break;
            default:
                playerRankingText.text += "th";
                break;
        }
    }
}
