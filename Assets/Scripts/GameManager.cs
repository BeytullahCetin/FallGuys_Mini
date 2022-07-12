using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GamePhase { Runner, WallPaint };

    public static GameManager Instance { get; private set; }

    [SerializeField] Transform player;

    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera wallCam;
    [SerializeField] Transform wallTransform;

    bool isLevelCompleted = false;
    GamePhase currentPhase;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentPhase = GamePhase.Runner;
    }

    public void CompleteLevel()
    {
        switch (currentPhase)
        {
            case GamePhase.Runner:
                currentPhase = GamePhase.WallPaint;
                playerCam.enabled = false;
                DisableAllComponents(player);

                break;

            case GamePhase.WallPaint:
                break;

        }
    }

    void DisableAllComponents(Transform tranformToDisable)
    {
        foreach (MonoBehaviour monoBehaviour in tranformToDisable.GetComponents<MonoBehaviour>())
        {
            monoBehaviour.enabled = false;
        }
    }
}
