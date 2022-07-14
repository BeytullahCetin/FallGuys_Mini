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
    [SerializeField] GameObject restartButton;

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


    private void OnEnable()
    {
        Drawable.OnWallPaintedChanged += ReceiveWallPaintPercentage;
    }

    private void OnDisable()
    {
        Drawable.OnWallPaintedChanged -= ReceiveWallPaintPercentage;
    }

    private void ReceiveWallPaintPercentage(int obj)
    {
        wallPaintPercentageText.SetText(obj.ToString());

        if (obj >= paintPercentageToWin)
        {
            CompleteLevel();
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
        StartCoroutine(DisableAllComponents(playerTransform));
        playerCamera.enabled = false;
        Camera.main.orthographic = true;
        wall.SetActive(true);
        wallPaintPercentageText.gameObject.SetActive(true);

        currentPhase = GamePhase.WallPaint;
    }

    void EndGame()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DisableAllComponents(Transform tranformToDisable)
    {
        yield return new WaitForSeconds(2f);

        foreach (MonoBehaviour monoBehaviour in tranformToDisable.GetComponents<MonoBehaviour>())
        {
            monoBehaviour.enabled = false;
        }
    }
}
