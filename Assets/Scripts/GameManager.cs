using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using FreeDraw;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GamePhase { Runner, WallPaint };

    public event Action OnWallPhaseStarted = delegate { };

    [SerializeField] Transform playerTransform;
    [SerializeField] CinemachineVirtualCamera playerCamera;

    [SerializeField] GameObject wall;
    [SerializeField] int paintPercentageToWin;
    [SerializeField] TextMeshProUGUI wallPaintPercentageText;
    [SerializeField] GameObject paintInfoText;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject restartButton;

    bool isInfoTextActive = false;
    GamePhase currentPhase;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        Drawable.OnWallPaintedChanged += ReceiveWallPaintPercentage;
    }

    private void OnDisable()
    {
        Drawable.OnWallPaintedChanged -= ReceiveWallPaintPercentage;
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
                StartWallPaintPhase();
                break;

            case GamePhase.WallPaint:
                EndGame();
                break;

        }
    }

    void StartWallPaintPhase()
    {
        OnWallPhaseStarted();
        DisableAllComponents(playerTransform);

        wall.SetActive(true);
        paintInfoText.SetActive(true);
        wallPaintPercentageText.gameObject.SetActive(true);

        isInfoTextActive = true;
        playerCamera.enabled = false;
        Camera.main.orthographic = true;

        currentPhase = GamePhase.WallPaint;
    }

    void DisableAllComponents(Transform tranformToDisable)
    {
        foreach (MonoBehaviour monoBehaviour in tranformToDisable.GetComponents<MonoBehaviour>())
        {
            monoBehaviour.enabled = false;
        }
    }

    private void ReceiveWallPaintPercentage(int obj)
    {
        if (isInfoTextActive)
        {
            isInfoTextActive = false;
            paintInfoText.SetActive(false);
        }

        wallPaintPercentageText.SetText(obj.ToString());

        if (obj >= paintPercentageToWin)
        {
            CompleteLevel();
        }
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void EndGame()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
